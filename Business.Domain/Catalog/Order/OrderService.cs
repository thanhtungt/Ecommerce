using Business.Models;
using Data.BaseRepository;
using Data.Entity.Enums;
using Data.Entity.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Utilities.ServiceResult;

namespace Business.Domain.Catalog.OrderNP
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<bool>> CreateOrderAsync(Guid userId, int addressId)
        {
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                /* var productincart = await _unitOfWork.CartRepository.GetManyAsync(x=>x.UserId)*/

                var query = from c in await _unitOfWork.CartRepository.GetAllAsync()
                            join p in await _unitOfWork.ProductRepository.GetAllAsync() on c.ProductId equals p.Id
                            where c.UserId == userId
                            select new { c, p };
                var order = new Order()
                {
                    CreateAt = DateTime.Now,
                    UserId = userId,
                    OrderStatus = OrderStatus.INPROGRESS,
                    UserOrderInfoId = addressId
                };

                await _unitOfWork.OrderRepository.AddAsync(order);

                await _unitOfWork.SaveChanageAsync();

                var orderDetail = new List<OrderDetail>();
                var cart = new List<Cart>();

                foreach (var x in query)
                {
                    var orderdetail = new OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = x.p.Id,
                        Quantity = x.c.Quantity,
                        Price = x.p.Price - (x.p.Price * (x.p.DiscountPercent / 100))
                    };
                    orderDetail.Add(orderdetail);

                    cart.Add(x.c);
                }
                _unitOfWork.CartRepository.RemoveRange(cart);

                await _unitOfWork.OrderDetailRepository.AddRangeAsync(orderDetail);

                await _unitOfWork.SaveChanageAsync();

                await _unitOfWork.CommitAsync();
                return new ServiceSuccessResult<bool>();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> CreateUserOrderInfoAsync(UserOrderInformationEntity model)
        {
            try
            {
                var info = new UserOrderInformation()
                {
                    UserId = model.UserId,
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Province = model.Province,
                    District = model.District,
                    Ward = model.Ward,
                    Address = model.Address,
                    IsDefault = model.IsDefault,
                    AddressType = model.AddressType,
                };
                if (model.IsDefault)
                {
                    var infos = await _unitOfWork.UserOrderInformationRepository.GetManyAsync(x => x.UserId == model.UserId);
                    foreach (var i in infos)
                    {
                        if (i.IsDefault == true) i.IsDefault = false;
                    }
                }

                await _unitOfWork.UserOrderInformationRepository.AddAsync(info);

                await _unitOfWork.SaveChanageAsync();

                return new ServiceSuccessResult<bool>(true);
            }catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> DeleteOrderAsync(int orderId)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                _unitOfWork.OrderRepository.Remove(order);
                await _unitOfWork.SaveChanageAsync();
                new ServiceSuccessResult<bool>(true);
            }catch (Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }


            throw new NotImplementedException();
        }

        public async Task<ServiceResult<bool>> DeleteUserOrderInfoAsync(int id)
        {
            try
            {
                var info = await _unitOfWork.UserOrderInformationRepository.GetByIdAsync(id);
                _unitOfWork.UserOrderInformationRepository.Remove(info);
                await _unitOfWork.SaveChanageAsync();

                return new ServiceSuccessResult<bool>();
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }

            throw new NotImplementedException();
        }

        public async Task<ServiceResult<IEnumerable<UserOrderInformationEntity>>> GetListUserOrderInfoAsync(Guid userId)
        {
            try
            {
                var infos = await _unitOfWork.UserOrderInformationRepository.GetManyAsync(x => x.UserId == userId);

                var vdaw = infos.Select(x => new UserOrderInformationEntity()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    Name = x.Name,
                    PhoneNumber = x.PhoneNumber,
                    Province = x.Province,
                    District = x.District,
                    Ward = x.Ward,
                    Address = x.Address,
                    IsDefault = x.IsDefault,
                    AddressType = x.AddressType,
                });

                return new ServiceSuccessResult<IEnumerable<UserOrderInformationEntity>>(vdaw);
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<UserOrderInformationEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<OrderResponseEntity>>> GetOrderAsync()
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetAllAsync();

                var result = new List<OrderResponseEntity>();

                foreach (var order in orders)
                {
                    var userorderinfo = await _unitOfWork.UserOrderInformationRepository.GetByIdAsync(order.UserOrderInfoId);

                    var porduct = from od in await _unitOfWork.OrderDetailRepository.GetAllAsync()
                                  join p in await _unitOfWork.ProductRepository.GetAllAsync() on od.ProductId equals p.Id
                                  where od.OrderId == order.Id
                                  select new OrderProductResponseEntity()
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Description = p.Description,
                                      Price = od.Price,
                                      Quantity = od.Quantity,
                                      ProductImagePath = p.ProductImagePath
                                  };

                    var item = new OrderResponseEntity()
                    {
                        UserOrderInfo = new UserOrderInformationEntity()
                        {
                            Id = userorderinfo.Id,
                            UserId = order.UserId,
                            Name = userorderinfo.Name,
                            PhoneNumber = userorderinfo.PhoneNumber,
                            Province = userorderinfo.Province,
                            District = userorderinfo.District,
                            Ward = userorderinfo.Ward,
                            Address = userorderinfo.Address,
                            IsDefault = userorderinfo.IsDefault,
                            AddressType = userorderinfo.AddressType,
                        },
                        Products = porduct,
                        OrderStatus = order.OrderStatus,
                        CreateAt = order.CreateAt,
                        OrderId = order.Id
                    };

                    result.Add(item);
                }

                return new ServiceSuccessResult<IEnumerable<OrderResponseEntity>>(result);
            }
            catch (Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<OrderResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<OrderResponseEntity>>> GetOrderByUserIdAsync(Guid userId)
        {
            try
            {
                var orders = await _unitOfWork.OrderRepository.GetManyAsync(x => x.UserId == userId);

                var result = new List<OrderResponseEntity>();

                foreach (var order in orders)
                {
                    var userorderinfo = await _unitOfWork.UserOrderInformationRepository.GetByIdAsync(order.UserOrderInfoId);

                    var porduct = from od in await _unitOfWork.OrderDetailRepository.GetAllAsync()
                                  join p in await _unitOfWork.ProductRepository.GetAllAsync() on od.ProductId equals p.Id
                                  where od.OrderId == order.Id
                                  select new OrderProductResponseEntity()
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Description = p.Description,
                                      Price = od.Price,
                                      Quantity = od.Quantity,
                                      ProductImagePath = p.ProductImagePath
                                  };

                    var item = new OrderResponseEntity()
                    {
                        UserOrderInfo = new UserOrderInformationEntity()
                        {
                            Id = userorderinfo.Id,
                            UserId = order.UserId,
                            Name = userorderinfo.Name,
                            PhoneNumber = userorderinfo.PhoneNumber,
                            Province = userorderinfo.Province,
                            District = userorderinfo.District,
                            Ward = userorderinfo.Ward,
                            Address = userorderinfo.Address,
                            IsDefault = userorderinfo.IsDefault,
                            AddressType = userorderinfo.AddressType,
                        },
                        Products = porduct,
                        OrderStatus = order.OrderStatus,
                        CreateAt = order.CreateAt,
                        OrderId = order.Id
                    };

                    result.Add(item);
                }

                return new ServiceSuccessResult<IEnumerable<OrderResponseEntity>>(result);
            }catch(Exception ex)
            {
                return new ServiceErrorResult<IEnumerable<OrderResponseEntity>>(ex.Message);
            }
        }

        public async Task<ServiceResult<bool>> SetOrderStatusAsync(int orderId, OrderStatus type)
        {
            try
            {
                var order = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                order.OrderStatus = type;
                await _unitOfWork.SaveChanageAsync();
                return new ServiceSuccessResult<bool>();
            }
            catch(Exception ex)
            {
                return new ServiceErrorResult<bool>(ex.Message);
            }
        }
    }
}

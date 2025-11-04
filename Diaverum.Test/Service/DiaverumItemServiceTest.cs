//using Diaverum.Data;
//using Diaverum.Domain;
//using Diaverum.Service;
//using Diaverum.Service.CustomeException;
//using Diaverum.Test.Helper;
//using Diaverum.Test.Helper.Domain;
//using Microsoft.EntityFrameworkCore;

//namespace Diaverum.Test.Service
//{
//    public class DiaverumItemServiceTest : ServiceTest
//    {
//        public class AddDiaverumItem
//        {
//            [Fact]
//            public async Task PasswordMissing()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitem = DiaverumItemHelper.New(password: null);

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.AddDiaverumItemAsync(diaverumitem));

//                // Assert
//                Assert.Equal(ExceptionType.InvalidRequest, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItemDTO.Password), exception.Message);
//            }

//            [Fact]
//            public async Task DiaverumItemnameExists()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemname = "TestDiaverumItem";
//                var diaverumitemDb = DiaverumItemHelper.NewDb(diaverumitemname: diaverumitemname);
//                await dbContext.DiaverumItems.AddAsync(diaverumitemDb);
//                await dbContext.SaveChangesAsync();

//                var diaverumitem = DiaverumItemHelper.New(diaverumitemname: diaverumitemname);

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.AddDiaverumItemAsync(diaverumitem));

//                // Assert
//                Assert.Equal(ExceptionType.ItemAlreadyExist, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItem.DiaverumItemname), exception.Message);
//                Assert.Contains(diaverumitemname, exception.Message);
//            }

//            [Fact]
//            public async Task Success()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitem = DiaverumItemHelper.New(id: null);

//                // Act
//                var result = await diaverumitemService.AddDiaverumItemAsync(diaverumitem);

//                // Assert
//                var dbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == 1);
//                Assert.NotNull(dbDiaverumItem);
//                Assert.Equal(dbDiaverumItem.CreatedBy, _cacheDiaverumItemId);
//                Assert.True(dbDiaverumItem.CreatedAt > DateTime.UtcNow.AddMinutes(-1) && dbDiaverumItem.CreatedAt < DateTime.UtcNow.AddMinutes(1));
//            }
//        }

//        public class GetDiaverumItems
//        {
//            [Fact]
//            public async Task EmptyResult()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                // Act
//                var result = await diaverumitemService.GetDiaverumItemsAsync();

//                // Assert
//                Assert.Null(result);
//            }

//            [Fact]
//            public async Task Success()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitem1 = DiaverumItemHelper.NewDb(id: 1, diaverumitemname: "DiaverumItem1");
//                var diaverumitem2 = DiaverumItemHelper.NewDb(id: 2, diaverumitemname: "DiaverumItem2");
//                await dbContext.DiaverumItems.AddAsync(diaverumitem1);
//                await dbContext.DiaverumItems.AddAsync(diaverumitem2);
//                await dbContext.SaveChangesAsync();

//                // Act
//                var result = await diaverumitemService.GetDiaverumItemsAsync();

//                // Assert
//                Assert.NotNull(result);
//                Assert.Equal(2, result.Count);
//                Assert.Contains(result, _ => _.Id == diaverumitem1.Id && _.DiaverumItemname == diaverumitem1.DiaverumItemname);
//                Assert.Contains(result, _ => _.Id == diaverumitem2.Id && _.DiaverumItemname == diaverumitem2.DiaverumItemname);
//            }
//        }

//        public class GetDiaverumItem
//        {
//            [Fact]
//            public async Task DiaverumItemNotFound()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb = DiaverumItemHelper.NewDb();
//                await dbContext.DiaverumItems.AddAsync(diaverumitemDb);
//                await dbContext.SaveChangesAsync();

//                var requestId = diaverumitemDb.Id + 1;

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.GetDiaverumItemAsync(requestId));

//                // Assert
//                Assert.Equal(ExceptionType.ItemNotFound, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItem.Id), exception.Message);
//                Assert.Contains(requestId.ToString(), exception.Message);
//            }

//            [Fact]
//            public async Task Success()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb1 = DiaverumItemHelper.NewDb(id: 1, diaverumitemname: "diaverumitem1");
//                var diaverumitemDb2 = DiaverumItemHelper.NewDb(id: 2, diaverumitemname: "diaverumitem2");
//                await dbContext.DiaverumItems.AddRangeAsync(diaverumitemDb1, diaverumitemDb2);
//                await dbContext.SaveChangesAsync();

//                // Act
//                var result = await diaverumitemService.GetDiaverumItemAsync(diaverumitemDb2.Id);

//                // Assert
//                Assert.NotNull(result);
//                Assert.Equal(diaverumitemDb2.Id, result.Id);
//                Assert.Equal(diaverumitemDb2.DiaverumItemname, result.DiaverumItemname);
//                Assert.Equal(diaverumitemDb2.RoleId, (short)result.RoleId);
//            }
//        }

//        public class UpdateDiaverumItem
//        {
//            [Fact]
//            public async Task DiaverumItemNotFound()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb = DiaverumItemHelper.NewDb();
//                await dbContext.DiaverumItems.AddAsync(diaverumitemDb);
//                await dbContext.SaveChangesAsync();

//                var diaverumitem = DiaverumItemHelper.New(id: diaverumitemDb.Id + 1);

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.UpdateDiaverumItemAsync(diaverumitem));

//                // Assert
//                Assert.Equal(ExceptionType.ItemNotFound, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItem.Id), exception.Message);
//                Assert.Contains(nameof(diaverumitem.Id), exception.Message);
//            }

//            [Fact]
//            public async Task DiaverumItemnameExists()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb1 = DiaverumItemHelper.NewDb(id: 1, diaverumitemname: "diaverumitem1");
//                var diaverumitemDb2 = DiaverumItemHelper.NewDb(id: 2, diaverumitemname: "diaverumitem2");
//                await dbContext.DiaverumItems.AddRangeAsync(diaverumitemDb1, diaverumitemDb2);
//                await dbContext.SaveChangesAsync();

//                var diaverumitem = DiaverumItemHelper.New(id: diaverumitemDb1.Id, diaverumitemname: diaverumitemDb2.DiaverumItemname);

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.UpdateDiaverumItemAsync(diaverumitem));

//                // Assert
//                Assert.Equal(ExceptionType.ItemAlreadyExist, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItem.DiaverumItemname), exception.Message);
//                Assert.Contains(nameof(diaverumitem.DiaverumItemname), exception.Message);
//            }

//            [Fact]
//            public async Task Success()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb1 = DiaverumItemHelper.NewDb(id: 1, diaverumitemname: "diaverumitem1");
//                var diaverumitemDb2 = DiaverumItemHelper.NewDb(id: 2, diaverumitemname: "diaverumitem2", roleId: Diaverum.Common.Role.DiaverumItem);
//                await dbContext.DiaverumItems.AddRangeAsync(diaverumitemDb1, diaverumitemDb2);
//                await dbContext.SaveChangesAsync();

//                var newDiaverumItemname = "newDiaverumItemname";
//                var newRole = Diaverum.Common.Role.Admin;
//                var diaverumitem = DiaverumItemHelper.New(id: diaverumitemDb2.Id, diaverumitemname: newDiaverumItemname, roleId: newRole);

//                // Act
//                var result = await diaverumitemService.UpdateDiaverumItemAsync(diaverumitem);

//                // Assert
//                var updateDbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumitemDb2.Id);
//                Assert.NotNull(result);
//                Assert.Equal(newDiaverumItemname, result.DiaverumItemname);
//                Assert.Equal(newRole, result.RoleId);

//                Assert.NotNull(updateDbDiaverumItem);
//                Assert.Equal(newDiaverumItemname, updateDbDiaverumItem.DiaverumItemname);
//                Assert.Equal((short)newRole, updateDbDiaverumItem.RoleId);

//                Assert.Equal(updateDbDiaverumItem.UpdatedBy, _cacheDiaverumItemId);
//                Assert.True(updateDbDiaverumItem.UpdatedAt > DateTime.UtcNow.AddMinutes(-1) && updateDbDiaverumItem.UpdatedAt < DateTime.UtcNow.AddMinutes(1));
//            }
//        }

//        public class DeleteDiaverumItem
//        {
//            [Fact]
//            public async Task DiaverumItemNotFound()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb = DiaverumItemHelper.NewDb();
//                await dbContext.DiaverumItems.AddAsync(diaverumitemDb);
//                await dbContext.SaveChangesAsync();

//                var requestId = diaverumitemDb.Id + 1;

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.DeleteDiaverumItemAsync(requestId));

//                // Assert
//                Assert.Equal(ExceptionType.ItemNotFound, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItem.Id), exception.Message);
//                Assert.Contains(requestId.ToString(), exception.Message);
//            }

//            [Fact]
//            public async Task Success()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb1 = DiaverumItemHelper.NewDb(id: 1, diaverumitemname: "diaverumitem1");
//                var diaverumitemDb2 = DiaverumItemHelper.NewDb(id: 2, diaverumitemname: "diaverumitem2");
//                await dbContext.DiaverumItems.AddRangeAsync(diaverumitemDb1, diaverumitemDb2);
//                await dbContext.SaveChangesAsync();

//                // Act
//                await diaverumitemService.DeleteDiaverumItemAsync(diaverumitemDb2.Id);

//                // Assert
//                var updateDbDiaverumItem1 = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumitemDb1.Id);
//                var updateDbDiaverumItem2 = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumitemDb2.Id);
//                Assert.NotNull(updateDbDiaverumItem1);
//                Assert.Null(updateDbDiaverumItem2);
//            }
//        }

//        public class GetDiaverumItemSecurityCredentials
//        {
//            [Fact]
//            public async Task DiaverumItemNotFound()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb = DiaverumItemHelper.NewDb();
//                await dbContext.DiaverumItems.AddAsync(diaverumitemDb);
//                await dbContext.SaveChangesAsync();

//                var requesDiaverumItemname = diaverumitemDb.DiaverumItemname + "1";

//                // Act
//                var exception = await Assert.ThrowsAsync<ServiceException>(() =>
//                    diaverumitemService.GetDiaverumItemSecurityCredentialsAsync(requesDiaverumItemname));

//                // Assert
//                Assert.Equal(ExceptionType.ItemNotFound, exception.ExceptionType);
//                Assert.Contains(nameof(DiaverumItem), exception.Message);
//                Assert.Contains(nameof(DiaverumItem.DiaverumItemname), exception.Message);
//                Assert.Contains(requesDiaverumItemname.ToString(), exception.Message);
//            }

//            [Fact]
//            public async Task Success()
//            {
//                // Arrange
//                var dbContext = await DatabaseHelper.GetContextAsync();
//                var cacheService = GetCacheService();
//                var diaverumitemService = new DiaverumItemService(dbContext, cacheService, _mapper);

//                var diaverumitemDb1 = DiaverumItemHelper.NewDb(id: 1, diaverumitemname: "diaverumitem1");
//                var diaverumitemDb2 = DiaverumItemHelper.NewDb(id: 2, diaverumitemname: "diaverumitem2");
//                await dbContext.DiaverumItems.AddRangeAsync(diaverumitemDb1, diaverumitemDb2);
//                await dbContext.SaveChangesAsync();

//                // Act
//                var result = await diaverumitemService.GetDiaverumItemSecurityCredentialsAsync(diaverumitemDb2.DiaverumItemname);

//                // Assert
//                Assert.NotNull(result);
//                Assert.Equal(diaverumitemDb2.RoleId, (short)result.RoleId);
//                Assert.Equal(diaverumitemDb2.PasswordHash, result.PasswordHash);
//                Assert.Equal(diaverumitemDb2.PasswordHashSalt, result.PasswordHashSalt);
//            }
//        }
//    }
//}

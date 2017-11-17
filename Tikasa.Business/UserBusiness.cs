using MyFinance.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tikasa.Data;
using Tikasa.Entities;
using Tikasa.Model;

namespace Tikasa.Business
{
    public interface IUserBusiness
    {
        bool Register(UserRegisterDTO model);
    }
    public class UserBusiness: BusinessBase, IUserBusiness
    {
        #region Contructor
        public UserBusiness(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

        }
        #endregion

        #region Properties
        private readonly IUnitOfWork unitOfWork;
        #endregion
        #region Methods
        public bool Register (UserRegisterDTO model)
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
            var userRepository = unitOfWork.Repository<User>();
            var IsExisted = userRepository.GetQueryable().Where(a => (a.Email == model.Email || a.UserName == model.UserName) 
            && a.StatusId!=(int)UserStatus.Deleted).FirstOrDefault() != null;
            if (IsExisted) {
                base.AddError("Email hoặc tên đăng nhập đã tồn tại");
                return false;
            }

            var row = new User()
            {
                Email=model.Email,
                UserName=model.UserName,
                CreatedDate=DateTime.Now,
                StatusId=(int)UserStatus.New
            };

            userRepository.Add(row);
            unitOfWork.Commit();
            return !this.HasError;
        }
        #endregion
    }
}

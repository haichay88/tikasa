using MyFinance.Core;
using MyFinance.Utils;
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
        string Register(UserRegisterDTO model);
        string Login(UserRegisterDTO model);
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
        public string Register (UserRegisterDTO model)
        {
            try
            {
                var userRepository = unitOfWork.Repository<User>();
                var IsExisted = userRepository.GetQueryable().Where(a => (a.Email == model.Email || a.UserName == model.UserName)
                && a.StatusId != (int)UserStatus.Deleted).FirstOrDefault() != null;
                if (IsExisted)
                {
                    base.AddError("Email hoặc tên đăng nhập đã tồn tại");
                    return string.Empty;
                }
               
                var row = new User()
                {
                    Email = model.Email,
                    UserName = model.UserName,
                    CreatedDate = DateTime.Now,
                    StatusId = (int)UserStatus.New,
                    Password=model.Password
                };
                userRepository.Add(row);
                unitOfWork.Commit();

                var user = userRepository.GetQueryable().Where(a => a.Id == row.Id).FirstOrDefault();
                var context = new Model.UserContext()
                {
                    UserId= user.Id,
                    Avatar=user.Avatar,
                    Email=user.Email,
                    UserName= user.UserName
                };
                string token = EncryptDecryptUtility.Encrypt(XmlUtility.Serialize(context),true);

                user.AccessToken = token;
                userRepository.Update(user);
                unitOfWork.Commit();

                return token;
            }
            catch (Exception)
            {
                base.AddError("Có lỗi trong quá trình đăng ký");
                return string.Empty;
            }
            
        }


        public string Login(UserRegisterDTO model)
        {
            try
            {
                var userRepository = unitOfWork.Repository<User>();
                var user = userRepository.GetQueryable().Where(a => ((a.Email == model.UserName || a.UserName == model.UserName) && a.Password==model.Password)
                && a.StatusId != (int)UserStatus.Deleted).FirstOrDefault() ;
                if (user==null)
                {
                    base.AddError("Đăng nhập không thành công !");
                    return string.Empty;
                }
                var context = new Model.UserContext()
                {
                    UserId = user.Id,
                    Avatar = user.Avatar,
                    Email = user.Email,
                    UserName = user.UserName
                };
                string token = EncryptDecryptUtility.Encrypt(XmlUtility.Serialize(context), true);

                user.AccessToken = token;
                user.LastLogin = DateTime.Now;
                userRepository.Update(user);
                unitOfWork.Commit();

                return token;
            }
            catch (Exception)
            {
                base.AddError("Có lỗi trong quá trình đăng ký");
                return string.Empty;
            }

        }
        #endregion




    }
}


using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Rental.Domain.Constants;
using Rental.Domain.Entities;
using Rental.Domain.Entities.Request;
using Rental.Domain.Entities.Response;
using Rental.Domain.Interfaces;
using Rental.Infrastructure.Persistence;
using Rental.Infrastructure.Utility;
using System.Linq.Expressions;
using Tools;
using Tools.Models;
using Tools.Models.Response;
using static Tools.Models.sort_direction;

namespace Rental.Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private AppDBContext _dbcontext;
        private IMapper _mapper;
        public UserRepository(ILogger<UserRepository> logger, AppDBContext dbcontext, IMapper mapper)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _mapper = mapper;
        }
        public async Task<result_response> AddUser(User user_request, string password_request, string user_auth, string credentials_value)
        {
            result_response result = new result_response();
            User _new_user = new User();
            string userDetails = "";
            DateTime datetimenow = DateTime.Now;
            try
            {
                // validate authentication header
                var checkToken = await _dbcontext.users_jwt_.Where(x => x.user_id == user_auth && x.user_token == credentials_value).AsNoTracking().FirstOrDefaultAsync();
                if (checkToken == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Token, ReturnMessage.Authorization);
                    return result;
                }
                //check user
                var _User = await _dbcontext.users_.Where(x => x.user_id == user_auth).AsNoTracking().FirstOrDefaultAsync();
                if (_User == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }
                // check duplicate UserID
                var _search_user = await _dbcontext.users_.Where(w => w.user_id == user_request.user_id).AsNoTracking().FirstOrDefaultAsync();
                if (_search_user != null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.UserIdExist);
                    return result;
                }
                //check if email UserID
                var _search_email = await _dbcontext.users_.Where(w => w.email == user_request.email).AsNoTracking().FirstOrDefaultAsync();
                if (_search_email?.email != null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.EmailExist);
                    return result;
                }
                //check if user is active
                else if (!_User.status)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.UserDeactivated);
                    return result;
                }
                // validate UserID
                if (!Helper.isAlphaNumeric(user_request.user_id!))
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserIdSpecialCharacter);
                    return result;
                }
                // check Access Right is correct
                if ((user_request.is_user) && !user_request.is_owner && !user_request.is_user)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.AccessRights);
                    return result;
                }
                //override rights
                if (user_auth == AuthConstant.Roles.admin)
                {
                    user_request.is_admin = false;
                    user_request.is_owner = true;
                    user_request.is_user = false;
                }
                else if (user_auth == AuthConstant.Roles.owner)
                {
                    user_request.is_admin = false;
                    user_request.is_owner = false;
                    user_request.is_user = true;
                }
                else if (user_auth == AuthConstant.Roles.user)
                {
                    user_request.is_admin = false;
                    user_request.is_owner = false;
                    user_request.is_user = true;
                }
                var salt_builder = Convert.ToBase64String(Hasher.getSalt());
                var myHashPass = Hasher.GetPasswordHash(salt_builder, AuthConstant.defaultAdmin.password);

                _new_user = new User()
                {
                    id = new Guid(),
                    user_id = user_request.user_id,
                    salt = salt_builder,
                    password = myHashPass,
                    email = user_request.email,
                    mobile = user_request.mobile,
                    first_name = user_request.first_name,
                    middle_name = user_request.middle_name,
                    last_name = user_request.last_name,
                    address = user_request.address,
                    status = true,
                    ip_lock = user_request.ip_lock,
                    last_online = datetimenow,
                    is_owner = user_request.is_owner,
                    is_admin = user_request.is_admin,
                    extra1 = "",
                    extra2 = "",
                    extra3 = "",
                    extra4 = "",
                    notes1 = "",
                    notes2 = "",
                    notes3 = "",
                    notes4 = "",
                    created_by = user_auth,
                    created_date = datetimenow,
                    updated_by = user_auth,
                    updated_date = datetimenow
                };
                userDetails = _new_user.toJSON();
                var identityResult = await _dbcontext.users_.AddAsync(_new_user);
                result = CreateResponse<result_response>(1, (int)ReturnCode.Success, ReturnMessage.Created, userDetails);
            }

            catch (Exception ex)
            {
                // Log the exception (not implemented here)
                _logger.LogError(ex, "An error occurred while creating a user.");
                result = CreateResponse<result_response>(1, (int)ReturnCode.Exception, ReturnMessage.ErrorException, ex.Message);
            }
            return result;
        }

        public async Task<users_response> GetUser(users_get_request request, string user_auth, string credentials_value)
        {
            users_response result = new users_response();

            try
            {
                // validate authentication header
                var checkToken = await _dbcontext.users_jwt_.Where(x => x.user_id == user_auth && x.user_token == credentials_value).AsNoTracking().FirstOrDefaultAsync();
                if (checkToken == null)
                {
                    result = CreateResponse<users_response>(2, (int)ReturnCode.Token, ReturnMessage.Authorization);
                    return result;
                }
                //check user
                var _User = await _dbcontext.users_.Where(x => x.user_id == user_auth).AsNoTracking().FirstOrDefaultAsync();
                if (_User == null)
                {
                    result = CreateResponse<users_response>(2, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }
                //check user is active
                else if (!_User.status)
                {
                    result = CreateResponse<users_response>(2, (int)ReturnCode.Error, ReturnMessage.UserDeactivated);
                    return result;
                }

                bool isGuid = Guid.TryParse(request.search, out Guid guid_id);

                if (_User.is_admin)
                {
                    var baseQuery = _dbcontext.users_.Where(r => request.search == null || r.id == guid_id || (r.email!.ToLower().Contains(request.search) || r.id == Guid.Parse(request.search) || (r.user_id!.ToLower().Contains(request.search))));
                    var fetchrecords_count = await baseQuery.CountAsync();
                    if (request.sort_by != null)
                    {

                        request.sort_by = request.sort_by!.ToLower();
                        var columnSelector = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { nameof(User.user_id).ToLower() , r => r.user_id.ToLower() },
                    { nameof(User.is_owner).ToLower(), r => r.is_owner },
                    { nameof(User.is_user).ToLower(), r => r.is_user },
                    { nameof(User.email).ToLower(), r => r.email.ToLower() }
                };

                        var selectedColumn = columnSelector[request.sort_by];

                        baseQuery = request.sort_direction == SortDirection.Ascending
                            ? baseQuery.OrderBy(selectedColumn)
                            : baseQuery.OrderByDescending(selectedColumn);
                    }

                    var page_count = (int)Math.Ceiling((double)fetchrecords_count / request.page_size);
                    if (request.page_no > page_count)
                    {
                        request.page_no = page_count;
                    }
                    var users = await baseQuery.Skip(request.page_size * (request.page_no - 1)).Take(request.page_size).ToListAsync();
                    var users_Response = _mapper.Map<List<user_map_response>>(users);
                    result = new users_response()
                    {
                        result_code = (int)ReturnCode.Success,
                        result_msg = ReturnMessage.Fetched,
                        userlist = users_Response,
                        no_of_records = fetchrecords_count, //totalCount
                        page_no = request.page_no,
                        page_size = request.page_size,
                        total_page_count = page_count == 0 ? 1 : page_count,
                        page_from = request.page_size * (request.page_no - 1) + 1, // The first item on the page
                        page_to = request.page_size * request.page_no // The last item on the page
                    };
                }
                else
                {

                    var baseQuery = _dbcontext.users_.Where(r => request.search == null && r.is_user && r.created_by == user_auth || r.id == guid_id && r.created_by == user_auth || (r.email!.ToLower().Contains(request.search) && r.is_user && r.created_by == user_auth || (r.user_id!.ToLower().Contains(request.search)) && r.is_user && r.created_by == user_auth));
                    var fetchrecords_count = await baseQuery.CountAsync();
                    if (request.sort_by != null)
                    {

                        request.sort_by = request.sort_by!.ToLower();
                        var columnSelector = new Dictionary<string, Expression<Func<User, object>>>
                {
                    { nameof(User.user_id).ToLower() , r => r.user_id.ToLower() },
                    { nameof(User.is_user).ToLower(), r => r.is_user },
                    { nameof(User.email).ToLower(), r => r.email.ToLower() }
                };

                        var selectedColumn = columnSelector[request.sort_by];

                        baseQuery = request.sort_direction == SortDirection.Ascending
                            ? baseQuery.OrderBy(selectedColumn)
                            : baseQuery.OrderByDescending(selectedColumn);
                    }

                    var page_count = (int)Math.Ceiling((double)fetchrecords_count / request.page_size);

                    var users = await baseQuery.Skip(request.page_size * (request.page_no - 1)).Take(request.page_size).ToListAsync();
                    var users_Response = _mapper.Map<List<user_map_response>>(users);
                    result = new users_response()
                    {
                        result_code = (int)ReturnCode.Success,
                        result_msg = ReturnMessage.Fetched,
                        userlist = users_Response,
                        no_of_records = fetchrecords_count, //totalCount
                        page_no = request.page_no,
                        page_size = request.page_size,
                        total_page_count = page_count == 0 ? 1 : page_count,
                    };
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching users.");
                return CreateResponse<users_response>(1, (int)ReturnCode.Exception, ReturnMessage.ErrorException, ex.Message);
            }
            return (result);
        }

        public async Task<result_response> UpdateUser(user_request request, string user_auth, string credentials_value)
        {
            result_response result = new result_response();
            List<change_log> _logs = new List<change_log>();
            DateTime datetimenow = DateTime.Now;
            string logdetails = "";

            try
            {
                // validate authentication header
                var checkToken = await _dbcontext.users_jwt_.Where(x => x.user_id == user_auth && x.user_token == credentials_value).AsNoTracking().FirstOrDefaultAsync();
                if (checkToken == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Token, ReturnMessage.Authorization);
                    return result;
                }
                //check user
                var _auth_user = await _dbcontext.users_.Where(x => x.user_id == user_auth).AsNoTracking().FirstOrDefaultAsync();
                if (_auth_user == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }
                //check user is active
                else if (!_auth_user.status)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.UserDeactivated);
                    return result;
                }

                var _update_user = await _dbcontext.users_.Where(x => x.user_id == request.user_id).FirstOrDefaultAsync();
                var _temp_update_user = await _dbcontext.users_.Where(x => x.user_id == request.user_id).AsNoTracking().FirstOrDefaultAsync();
                if (_update_user == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }

                if (user_auth != request.user_id)
                {
                    if (!AppChecker.has_right(_update_user, _auth_user))
                    {
                        result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.AccessRights);
                        return result;
                    }
                    _update_user.email = request.email;
                    _update_user.mobile = request.mobile;
                    _update_user.first_name = request.first_name;
                    _update_user.middle_name = request.middle_name;
                    _update_user.last_name = request.last_name;
                    _update_user.status = request.status;
                    _update_user.ip_lock = request.ip_lock;
                    _update_user.address = request.address;
                    _update_user.date_of_birth = request.date_of_birth;
                }

                _update_user.updated_by = user_auth;
                _update_user.updated_date = datetimenow;
                _logs = Helper.getChanges(_temp_update_user, _update_user);
                foreach (var log in _logs)
                {
                    logdetails += log.PropertyName + " value from: " + log.OldValue + " to: " + log.NewValue + "\r\n";
                }

                _dbcontext.Entry(_update_user).State = EntityState.Modified;
                await _dbcontext.SaveChangesAsync();

                result = CreateResponse<result_response>(1, (int)ReturnCode.Success, ReturnMessage.Updated, logdetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a user.");
                return CreateResponse<result_response>(1, (int)ReturnCode.Exception, ReturnMessage.ErrorException, ex.Message);
            }
            return result;
        }

        public async Task<result_response> DeleteUser(user_id_request request, string user_auth, string credentials_value)
        {
            result_response result = new result_response();
            List<change_log> _logs = new List<change_log>();
            DateTime datetimenow = DateTime.Now;
            string logdetails = "";

            try
            {
                // validate authentication header
                var checkToken = await _dbcontext.users_jwt_.Where(x => x.user_id == user_auth && x.user_token == credentials_value).AsNoTracking().FirstOrDefaultAsync();
                if (checkToken == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Token, ReturnMessage.Authorization);
                    return result;
                }
                //check user
                var _auth_user = await _dbcontext.users_.Where(x => x.user_id == user_auth).AsNoTracking().FirstOrDefaultAsync();
                if (_auth_user == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }
                //check user is active
                else if (!_auth_user.status)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.UserDeactivated);
                    return result;
                }

                var _delete_user = await _dbcontext.users_.Where(x => x.user_id == request.user_id).FirstOrDefaultAsync();

                if (_delete_user == null)
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.InvalidUserId);
                    return result;
                }
                if (!AppChecker.has_right(_delete_user, _auth_user))
                {
                    result = CreateResponse<result_response>(1, (int)ReturnCode.Error, ReturnMessage.AccessRights);
                    return result;
                }
                var _user_ip = await _dbcontext.users_ip_.Where(x => x.user_id == _delete_user.user_id).ToListAsync();
                var _user_jwt = await _dbcontext.users_jwt_.Where(x => x.user_id == _delete_user.user_id).ToListAsync();

                var deleteUserDetails = new
                {
                    user = _delete_user,
                    userips = _user_ip,
                    userjwt = _user_jwt
                };
                _dbcontext.users_ip_.RemoveRange(_user_ip);
                _dbcontext.users_jwt_.RemoveRange(_user_jwt);
                _dbcontext.users_.Remove(_delete_user);
                await _dbcontext.SaveChangesAsync();
                result = CreateResponse<result_response>(1, (int)ReturnCode.Success, ReturnMessage.Updated, deleteUserDetails.toJSON());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a user.");
                return CreateResponse<result_response>(1, (int)ReturnCode.Exception, ReturnMessage.ErrorException, ex.Message);
            }
            return result;
        }

        private T CreateResponse<T>(int whattodo, int code, string msg, object record = null!)
        {
            object result;

            switch (whattodo)
            {
                case 1:
                    result_response _result = new result_response()
                    {
                        result_code = code,
                        result_msg = msg,
                        details = record == null ? "" : record?.ToString()
                    };
                    result = _result;
                    break;
                case 2:
                    users_response _users_response = new users_response()
                    {
                        result_code = code,
                        result_msg = msg,
                        userlist = code == 0 ? (List<user_map_response>)record : new List<user_map_response>()
                    };
                    result = _users_response;
                    break;
                default:
                    base_response _base_response = new base_response()
                    {
                        result_code = code,
                        result_msg = msg
                    };
                    result = _base_response;
                    break;
            }
            return (T)result;
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_dbcontext != null)
                {
                    _dbcontext.Dispose();
                    _dbcontext = null!;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

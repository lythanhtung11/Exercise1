using Exercise1.Databases;
using Exercise1.Jobs;
using Exercise1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exercise1.Services.Users
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly JobSettings _jobSettings;
        private readonly int _maxThreads = 5;
        private readonly int _batchSize = 5;
        private int _pageNumber = 0;
        private int _pageSize = 200;
        private List<User> _usersList = null;

        public UserService(IUserRepository userRepository, IEmailService emailService, JobSettings jobSettings)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _jobSettings = jobSettings;
        }

        // Represents method to update user status
        public async Task UpdateUserStatusAsync()
        {
            var monthsToConsider = _jobSettings.MonthToPwdExpired;
            var referenceDate = DateTime.Now.AddMonths(monthsToConsider);

            //fectch users that have password expired
            _usersList = await _userRepository.GetUsersWithPasswordExpiredAsync(referenceDate);

            _pageNumber = 0;

            var totalBatchCount = CalculateTotalBatch(_usersList.Count());

            if (_usersList != null && totalBatchCount > 0)
            {
                int batchSize = _batchSize;

                if(batchSize > totalBatchCount)
                {
                   batchSize = totalBatchCount;
                }

                //Using SemaphoreSlim to limits threads 
                using (SemaphoreSlim semaphore = new SemaphoreSlim(_maxThreads))
                {
                    var tasks = new List<Task>();

                    for (int page = 1; page <= batchSize; page++) {

                        //Wait for threads available
                        await semaphore.WaitAsync(); 

                        var task = Task.Run(async () =>
                        {
                            try
                            {
                                List<User> result = DistributeUserList();

                                //Update user status and send email notification
                                foreach (var user in result)
                                {
                                    user.Status = UserStatus.REQUIRE_CHANGE_PWD;
                                    await _emailService.SendEmailAsync(user.Email, "Password Change Required",
                                        "Your password has not been updated in more than six months. Please update it as soon as possible.");
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine($"Error while updating data: {e.Message}");
                            }
                            finally
                            {
                                semaphore.Release();
                            }
                        });
                        tasks.Add(task);
                    }

                    await Task.WhenAll(tasks);
                }
            }

            await _userRepository.SaveChangesAsync();
        }

        // Represents method to distribute users
        private List<User> DistributeUserList()
        {
            _pageNumber++;
            return ReturnUserListBasedOnPage(_usersList, _pageNumber);
        }

        // Represents method to get users based on page number
        private List<User> ReturnUserListBasedOnPage(List<User> userList,int page)
        {
            List<User> newList = new List<User>();

            if(page >= 1)
            {
                int skip = _pageSize * (page - 1);
                newList = userList.Skip(skip).Take(_pageSize).ToList();
                
            }
            return newList;

        }

        //Represents method to calculate batch runs
        private int CalculateTotalBatch(int totalCount)
        {
            return (int)Math.Ceiling((double) totalCount / _pageSize);
        }


    }
}

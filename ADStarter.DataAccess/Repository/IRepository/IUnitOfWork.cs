﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADStarter.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ITherapistRepository Therapist { get; }
        IAdminRepository Admin { get; }
        IProgramRepository Program { get; }
        ICustomerServiceRepository CustomerService { get; }
        IAnnouncementRepository Announcement { get; }
        void Save();
    }
}

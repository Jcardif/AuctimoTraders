using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctimoTraders.Shared.Helpers
{
    public enum Gender
    {
        M,
        F
    }

    public enum UserRole
    {
        SalesPerson,
        CountryManager,
        RegionalManager
    }

    public enum SalesChannel
    {
        Online,
        Offline
    }

    public enum Quarter
    {
        Q1,
        Q2,
        Q3,
        Q4
    }

    public enum OrderPriority
    {
        C,H,L,M
    }
}

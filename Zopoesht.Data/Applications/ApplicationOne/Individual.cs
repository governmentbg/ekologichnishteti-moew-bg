﻿using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class Individual : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}

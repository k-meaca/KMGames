using KMGames.Web.ViewModel.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;

namespace KMGames.Web.ViewModel.Developers
{
    public class DeveloperDetailVm
    {
        public DeveloperListVm Developer { get; set; }

        public List<GameListVm> Games { get; set; }
    }
}
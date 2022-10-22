using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestStore.Application.Dto
{
    public class NavLinksUserDataDto
    {
        public List<NavLinkDto> NavLinks { get; set; }
        public string Username { get; set; }
    }
}

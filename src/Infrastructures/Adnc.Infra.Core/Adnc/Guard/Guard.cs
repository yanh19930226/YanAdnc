using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adnc.Infra.Core.Adnc.Guard
{
    public interface IGuard
    {
    }

    public class Guard : IGuard
    {
        public static IGuard Checker { get; } = new Guard();

        private Guard()
        { }
    }
}

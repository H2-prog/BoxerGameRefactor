using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoxerGamerRefactor
{
    public interface IBoxerGameAIHandler
    {
        BoxerAttack ChooseRandomAttackIn(IEnumerable<BoxerAttack> attacks);
    }

    public class BoxerGameAIHandler : IBoxerGameAIHandler
    {
        public BoxerAttack ChooseRandomAttackIn(IEnumerable<BoxerAttack> attacks)
        {
            var index = Utils.GetRandomIndexOfCollection(attacks);
            return attacks.ElementAtOrDefault(index);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MassCultureLibrary.Games
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException(Guid gameId)
            : base($"Game with ID {gameId} was not found.")
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CardsLibrary;

namespace CardGames
{
    [Serializable]
    public class TooManyPlayersException : Exception
    {
      public TooManyPlayersException() { }
      public TooManyPlayersException ( string message ) : base( message ) { }
      public TooManyPlayersException ( string message, Exception inner ) : base( message, inner ) { }
      protected TooManyPlayersException ( 
	    System.Runtime.Serialization.SerializationInfo info, 
	    System.Runtime.Serialization.StreamingContext context ) : base( info, context ) { }
    }
}
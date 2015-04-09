// WhistInfo.cs
// <copyright file="WhistInfo.cs"> This code is protected under the MIT License. </copyright>
using CardGames;
using CardsLibrary;

namespace Whist
{
    /// <summary>
    /// An extension of <see cref="GameInfo"/> that provides extra properties for <see cref="Whist"/>.
    /// </summary>
    public class WhistInfo : GameInfo
    {
        /// <summary>
        /// The internal value of Trumps.
        /// </summary>
        private Suit trumps;

        /// <summary>
        /// The internal value of FirstSuitLaid.
        /// </summary>
        private Suit firstSuitLaid;

        /// <summary>
        /// Gets or sets the value of trumps.
        /// </summary>
        public Suit Trumps 
        {   
            get
            {
                return this.trumps;
            }

            set
            {
                this.trumps = value;
                SuitOrder.SetTrumps(value);
            }
        }
        
        /// <summary>
        /// Gets or sets the value of the first suit laid.
        /// </summary>
        public Suit FirstSuitLaid
        {
            get
            {
                return this.firstSuitLaid;
            }

            set
            {
                this.firstSuitLaid = value;
                SuitOrder.SetPlayed(value);
            }
        }
    }
}

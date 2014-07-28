// ShedPlayer.cs
// <copyright file="ShedPlayer.cs"> This code is protected under the MIT License. </copyright>
using System.Collections.Generic;
using System.Linq;
using CardsLibrary;

namespace CardGames.Shed
{
    /// <summary>
    /// An abstract super-class of every <see cref="Shed"/> player.
    /// </summary>
    public abstract class ShedPlayer : Player<ShedInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShedPlayer" /> class's sub-classes.
        /// </summary>
        public ShedPlayer()
        {
            this.Hand = new List<Card>();
            this.Blinds = new List<Card>();
            this.Tables = new List<Card>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShedPlayer" /> class's sub-classes.
        /// </summary>
        /// <param name="cards"> The cards in the player's hand. </param>
        public ShedPlayer(IEnumerable<Card> cards)
        {
            this.Hand = cards.ToList();
            this.Blinds = new List<Card>();
            this.Tables = new List<Card>();
        }

        /// <summary>
        /// Gets or sets the Blind cards.
        /// </summary>
        public List<Card> Blinds { get; set; }

        /// <summary>
        /// Gets or sets the visible cards on the table.
        /// </summary>
        public List<Card> Tables { get; set; }

        /// <summary>
        /// Makes the appropriate move using the given arguments.
        /// </summary>
        /// <param name="args"> The current game info. </param>
        /// <param name="otherPlayers"> The other players so it can display their tables. </param>
        /// <param name="cardsPlayed"> The amount of cards played in the move (with ref keyword). </param>        
        /// <returns> The (last) card played. </returns>
        public abstract Card MakeMove(ShedInfo args, List<ShedPlayer> otherPlayers, ref int cardsPlayed);

        /// <summary>
        /// Orders the hand.
        /// </summary>
        protected override void OrderCards()
        {
            // Order a list of the cards in the hand using linq
            List<Card> orderedHand = this.Hand.OrderBy(c => (int)c.Value)         // Order first by value
                                              .ThenBy(c => (int)c.Suit).ToList(); // Then by suit

            // Make the hand become the ordered list
            this.Hand = new List<Card>(orderedHand);
        }
    }
}

// SuitOrder.cs
// <copyright file="SuitOrder.cs"> This code is protected under the MIT License. </copyright>
/* SUIT ORDER:
 *    1 = null
 *    2 = not played and not trump
 *    3 = suit above the nothing but not played still, its bais so tends not to be used
 *    4 = card played
 *    5 = trump
 */
namespace CardsLibrary
{
    /// <summary>
    /// A static class that will set the value of the suit
    /// within the Settings class.
    /// </summary>
    public static class SuitOrder
    {
        /// <summary>
        /// This resets all the suit values to 2 (not played and not a trump).
        /// </summary>
        public static void Reset() 
        {
            Settings.ClubsOrder = 2;
            Settings.DiamondsOrder = 2;
            Settings.SpadesOrder = 2;
            Settings.HeartsOrder = 2;
            Settings.NullOrder = 2;
        }

        /// <summary>
        /// This sets the specified suit to the value 1 (trumps).
        /// </summary>
        /// <param name="s"> This is of type "Suit" and is the suit that will be changed to trump value. </param>
        public static void SetTrumps(Suit s) 
        {
            if (s == Suit.Clubs) 
            { 
                Settings.ClubsOrder = 5; 
            }
            else if (s == Suit.Diamonds)
            {
                Settings.DiamondsOrder = 5;
            }
            else if (s == Suit.Spades) 
            { 
                Settings.SpadesOrder = 5; 
            }
            else if (s == Suit.Hearts)
            { 
                Settings.HeartsOrder = 5; 
            }
            else if (s == Suit.Null) 
            { 
                Settings.NullOrder = 5; 
            }
        }

        /// <summary>
        /// This sets the specified suit to value 2 (played).
        /// </summary>
        /// <param name="s"> This is of type "Suit" and is the suit that will be changed to played value. </param>
        public static void SetPlayed(Suit s) 
        {
            if (s == Suit.Clubs) 
            { 
                Settings.ClubsOrder = 4; 
            }
            else if (s == Suit.Diamonds) 
            { 
                Settings.DiamondsOrder = 4; 
            }
            else if (s == Suit.Spades) 
            { 
                Settings.SpadesOrder = 4; 
            }
            else if (s == Suit.Hearts)
            { 
                Settings.HeartsOrder = 4; 
            }
            else if (s == Suit.Null) 
            { 
                Settings.NullOrder = 4; 
            }
        }

        /// <summary>
        /// This sets the specified suit to value 3 (above not played or trumps, but below played).
        /// </summary>
        /// <param name="s"> This is of type "Suit" and is the suit that will be changed to third value. </param>
        /// <remarks> This is a method to make the game bias toward a suit so will not commonly be used. </remarks>
        public static void SetThird(Suit s)
        {
            if (s == Suit.Clubs)
            { 
                Settings.ClubsOrder = 3; 
            }
            else if (s == Suit.Diamonds) 
            { 
                Settings.DiamondsOrder = 3; 
            }
            else if (s == Suit.Spades) 
            { 
                Settings.SpadesOrder = 3; 
            }
            else if (s == Suit.Hearts) 
            {
                Settings.HeartsOrder = 3;
            }
            else if (s == Suit.Null)
            {
                Settings.NullOrder = 3; 
            }
        }

        /// <summary>
        /// This sets the specified suit to value 5 (Null).
        /// </summary>
        /// <param name="s"> This is of type "Suit" and is the suit that will be changed to Null value. </param>
        public static void SetNull(Suit s) 
        {
            if (s == Suit.Clubs) 
            { 
                Settings.ClubsOrder = 1; 
            }
            else if (s == Suit.Diamonds)
            { 
                Settings.DiamondsOrder = 1; 
            }
            else if (s == Suit.Spades) 
            {
                Settings.SpadesOrder = 1;
            }
            else if (s == Suit.Hearts)
            {
                Settings.HeartsOrder = 1; 
            }
            else if (s == Suit.Null)
            { 
                Settings.NullOrder = 1;
            }
        }
    }
}

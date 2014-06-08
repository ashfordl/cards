// Settings.cs
// <copyright file="Settings.cs"> This code is protected under the MIT License. </copyright>
namespace CardsLibrary
{
    /// <summary>
    /// A static class with all the data to do with the card games settings.
    /// </summary>
    /// <remarks> This includes if Ace is high, and the values of the suit, 
    /// i.e. if they are trumps, or the played suit etc. </remarks>
    public static class Settings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the Ace is high in this game/round.
        /// </summary>
        public static bool AceHigh { get; set; }

        /// <summary>
        /// Gets or sets the order value of the suit "Clubs".
        /// </summary>
        public static int ClubsOrder { get; set; }

        /// <summary>
        /// Gets or sets the order value of the suit "Diamonds".
        /// </summary>
        public static int DiamondsOrder { get; set; }

        /// <summary>
        /// Gets or sets the order value of the suit "Spades".
        /// </summary>
        public static int SpadesOrder { get; set; }

        /// <summary>
        /// Gets or sets the order value of the suit "Hearts".
        /// </summary>
        public static int HeartsOrder { get; set; }

        /// <summary>
        /// Gets or sets the order value of the suit "Null".
        /// </summary>
        public static int NullOrder { get; set; }
    }
}

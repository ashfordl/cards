// GermanWhistInfo.cs
// <copyright file="GermanWhistInfo.cs"> This code is protected under the MIT License. </copyright>
using CardsLibrary;

namespace CardGames.Whist.GermanWhist
{
    /// <summary>
    /// An extension of <see cref="GameInfo"/> that provides extra properties for <see cref="GermanWhist"/>.
    /// </summary>
    public class GermanWhistInfo : WhistInfo
    {
        /// <summary>
        /// Gets or sets the card that will be played for.
        /// </summary>
        public Card ToPlayFor { get; set; }
    }
}

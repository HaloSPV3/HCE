/**
 * Copyright (C) 2019 Emilian Roman
 * 
 * This file is part of SPV3.Domain.
 * 
 * SPV3.Domain is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * SPV3.Domain is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with SPV3.Domain.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace SPV3.Domain
{
    /// <summary>
    ///     Type for storing a semantic version value.
    /// </summary>
    public class Version
    {
        /// <summary>
        ///     <see cref="Major" />
        /// </summary>
        private int _major;

        /// <summary>
        ///     <see cref="Minor" />
        /// </summary>
        private int _minor;

        /// <summary>
        ///     <see cref="Patch" />
        /// </summary>
        private int _patch;

        /// <summary>
        ///     Value representing an API-breaking version change.
        /// </summary>
        public int Major
        {
            get => _major;
            set => _major = value;
        }

        /// <summary>
        ///     Value representing an API addition or change.
        /// </summary>
        public int Minor
        {
            get => _minor;
            set => _minor = value;
        }

        /// <summary>
        ///     Value representing a fix or patch.
        /// </summary>
        public int Patch
        {
            get => _patch;
            set => _patch = value;
        }

        /// <summary>
        ///     Represent object as string.
        /// </summary>
        /// <param name="version">
        ///     Object to represent as string.
        /// </param>
        /// <returns>
        ///     String representation of the object.
        /// </returns>
        public static implicit operator string(Version version)
        {
            return $"{version.Major}.{version.Minor}.{version.Patch}";
        }

        /// <summary>
        ///     Represent string as object.
        /// </summary>
        /// <param name="version">
        ///     String to represent as object.
        /// </param>
        /// <returns>
        ///     Object representation of the string.
        /// </returns>
        public static explicit operator Version(string version)
        {
            var split = version.Split('.');

            return new Version
            {
                Major = int.Parse(split[0]),
                Minor = int.Parse(split[1]),
                Patch = int.Parse(split[2])
            };
        }
    }
}
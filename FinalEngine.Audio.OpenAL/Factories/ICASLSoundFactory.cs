// <copyright file="ICASLSoundFactory.cs" company="Software Antics">
//     Copyright (c) Software Antics. All rights reserved.
// </copyright>

namespace FinalEngine.Audio.OpenAL.Factories;

using ICASLSound = CASL.ISound;

internal interface ICASLSoundFactory
{
    ICASLSound CreateSound(string filePath);
}

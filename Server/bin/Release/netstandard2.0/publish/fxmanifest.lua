fx_version 'cerulean'
game 'gta5'

name 'XCore Server'
author 'LifeOwner'
description 'FiveM Server with RabbitMQ Integration'
version '1.0.0'

server_scripts {
    'XCore.Server.net.dll'
}
files {
    "RabbitMQ.Client.dll",
    "CitizenFX.Core.Server.dll",
    "System.Buffers.dll",
    "System.Memory.dll",
    "System.Numerics.Vectors.dll",
    "System.Runtime.CompilerServices.Unsafe.dll"
}
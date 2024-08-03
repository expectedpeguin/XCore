
-- Manifest data
fx_version 'bodacious'
games {'gta5'}

-- Resource stuff
name 'XCore Server'
description 'XCore based server'
version 'v0.0.1'
author 'LifeOwner'
url 'https://github.com/expectedpeguin/XCore/'

-- Files & scripts
files {
    'Server/RabbitMQ.Client.dll'
}

server_script 'Server/XCore.Server.net.dll'
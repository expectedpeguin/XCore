@echo off
pushd Client
dotnet publish -c Release
popd

pushd Server
dotnet publish -c Release
popd

rmdir /s /q C:\Users\LifeO\Documents\txData\QBCoreFramework_AD3764.base\resources\[customs]\XCore
mkdir C:\Users\LifeO\Documents\txData\QBCoreFramework_AD3764.base\resources\[customs]\XCore

copy /y fxmanifest.lua C:\Users\LifeO\Documents\txData\QBCoreFramework_AD3764.base\resources\[customs]\XCore
xcopy /y /e Client\bin\Release\net452\publish C:\Users\LifeO\Documents\txData\QBCoreFramework_AD3764.base\resources\[customs]\XCore\Client
xcopy /y /e Server\bin\Release\netstandard2.0\publish C:\Users\LifeO\Documents\txData\QBCoreFramework_AD3764.base\resources\[customs]\XCore\Server
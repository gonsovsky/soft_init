var x = new ExpandedPrototype();
var result = x.installService('../../../WindowsService/bin/debug/WindowsService.exe','@WindowsServiceDemo');
console.log("JS said:" + result);
result;
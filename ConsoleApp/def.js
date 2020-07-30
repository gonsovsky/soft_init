
const api = new advapi32();


try {

    const a = new CreateServiceIn()
    a.lpSvcName = "@WindowsServiceDemo";
    a.dwDesiredAccess = a.SERVICE_ALL_ACCESS;
    a.dwServiceType = a.SERVICE_WIN32_OWN_PROCESS;
    a.dwStartType = a.SERVICE_AUTO_START;
    a.dwErrorControl = a.SERVICE_ERROR_NORMAL;
    a.lpPathName = "../../../WindowsService/bin/debug/WindowsService.exe";
    a.lpLoadOrderGroup = null;
    a.lpdwTagId = 0;
    a.lpDependencies = null;
    a.lpServiceStartName = null;
    a.lpPassword = null;

    //a.lpSvcName = null; //uncomment to force valiation error
    //a.dwDesiredAccess = 12345;
    //a.lpPathName = "C:\autoexec.batttt";

    const result = api.createService(a);
    console.log("JS result:" + result.result);
    console.log("JS errorCode:" + result.errorCode);
    console.log("JS message:" + result.message);
    result;
} catch (err) {

    console.log("JS exception: " + err);
}


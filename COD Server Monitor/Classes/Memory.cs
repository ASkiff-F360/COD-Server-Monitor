using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace COD_Server_Monitor
{
   public static class Memory
   {
      public static bool InjectDLL(Process targetProcess, string dllName)
      {
         IntPtr procHandle = Imports.OpenProcess(Imports.PROCESS_CREATE_THREAD | Imports.PROCESS_QUERY_INFORMATION | Imports.PROCESS_VM_OPERATION | Imports.PROCESS_VM_WRITE | Imports.PROCESS_VM_READ, false, targetProcess.Id);
         if (procHandle == null)
            return false;

         IntPtr loadLibraryAddr = Imports.GetProcAddress(Imports.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
         if (loadLibraryAddr == null)
            return false;

         IntPtr allocMemAddress = Imports.VirtualAllocEx(procHandle, IntPtr.Zero, (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), Imports.MEM_COMMIT | Imports.MEM_RESERVE, Imports.PAGE_READWRITE);
         if (allocMemAddress == null)
            return false;

         Int32 bytesWritten;
         if (!Imports.WriteProcessMemory(procHandle, allocMemAddress, Encoding.Default.GetBytes(dllName), (uint)((dllName.Length + 1) * Marshal.SizeOf(typeof(char))), out bytesWritten))
            return false;

         IntPtr r = Imports.CreateRemoteThread(procHandle, IntPtr.Zero, 0, loadLibraryAddr, allocMemAddress, 0, IntPtr.Zero);
         if (r == null)
            return false;

         return true;
      }
   }
}

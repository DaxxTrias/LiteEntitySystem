using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace LiteEntitySystem
{
    internal static class Utils
    {
#if UNITY_STANDALONE_WIN
        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
#else
        [DllImport("libc", CallingConvention = CallingConvention.Cdecl)]                
#endif
        public static extern unsafe int memcmp([In] void* b1, [In] void* b2, [In] UIntPtr count);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResizeIfFull<T>(ref T[] arr, int count)
        {
            if (count == arr.Length)
                Array.Resize(ref arr, count*2);
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ResizeOrCreate<T>(ref T[] arr, int count)
        {
            if (arr == null)
                arr = new T[count];
            else if (count >= arr.Length)
                Array.Resize(ref arr, count*2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsBitSet(byte[] byteArray, int offset, int bitNumber)
        {
            return (byteArray[offset + bitNumber / 8] & (1 << bitNumber % 8)) != 0;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe bool IsBitSet(byte* byteArray, int bitNumber)
        {
            return (byteArray[bitNumber / 8] & (1 << bitNumber % 8)) != 0;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsByteFlagSet<T>(this T flags, T flag) where T : Enum
        {
            return (Unsafe.As<T, byte>(ref flags) & Unsafe.As<T, byte>(ref flag)) != 0;
        }
    }
}
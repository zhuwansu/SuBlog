/*******************************************************
 * 
 * 作者：朱皖苏
 * 创建日期：20200829
 * 说明：此文件只包含一个类，具体内容见类型注释。
 * 运行环境：.NET Stanard 2.0
 * 版本号：1.0.0
 * 
 * 历史记录：
 * 创建文件 朱皖苏 20200829 11:51
 * 
*******************************************************/

using System;
using System.Threading;

namespace DbUtils
{
    public static class IdGenerator
    {

        private static readonly UUIDGenerator uuidGenerator = new UUIDGenerator();
        private static readonly SnowflakeIDGenerator snowflakeIDGenerator = new SnowflakeIDGenerator(IdGeneratorSetting.MachineId);

        static IdGenerator()
        {

        }


        public static long Int64()
        {
            return snowflakeIDGenerator.Generator();
        }

        public static Guid Guid()
        {
            return uuidGenerator.Generator();
        }



        private class UUIDGenerator
        {
            private static readonly DateTime BaseDate = new DateTime(1900, 1, 1);

            public Guid Generator()
            {
                byte[] guidArray = System.Guid.NewGuid().ToByteArray();
                var now = DateTime.Now;
                var days = new TimeSpan(now.Ticks - BaseDate.Ticks);
                var msecs = now.TimeOfDay;

                byte[] daysArray = BitConverter.GetBytes(days.Days);
                byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

                Array.Reverse(daysArray);
                Array.Reverse(msecsArray);

                Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
                Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

                return new Guid(guidArray);
            }

        }

        private class SnowflakeIDGenerator
        {
            private static readonly int machineIdBits = 5;
            private static readonly int datacenterIdBits = 5;
            private static readonly int sequenceBits = 12;
            private static readonly long offsetDateTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
            private static readonly long twepoch = (new DateTime(2011, 8, 1, 0, 0, 0, DateTimeKind.Utc).Ticks - offsetDateTimeTicks) / 10000L;
            private static readonly long maxMachineId = -1L ^ -1L << machineIdBits;
            private static readonly long maxDataCenterId = -1L ^ (-1 << datacenterIdBits);
            private static readonly long machineIdShift = sequenceBits;
            private static readonly long dataCenterIdShift = sequenceBits + machineIdBits;
            private static readonly long timestampLeftShift = sequenceBits + machineIdBits + datacenterIdBits;
            private static readonly long sequenceMask = -1L ^ -1L << sequenceBits;
            private long lastTimestamp = -1L;
            private long sequence = 0;
            private long dataCenterId = 0;
            private long machineId;
            private static SpinLock _lock = new SpinLock();

            public SnowflakeIDGenerator() : this(0, 0)
            {
            }

            public SnowflakeIDGenerator(int machineId) : this(machineId, 0)
            {
            }

            public SnowflakeIDGenerator(long machineId, long dataCenterId)
            {
                if (machineId < 0 || machineId > maxMachineId) throw new Exception("机器码ID非法");
                if (dataCenterId < 0 || dataCenterId > maxDataCenterId) throw new Exception("数据中心ID非法");
                this.machineId = machineId;
                this.dataCenterId = dataCenterId;
            }
            private static long GetTimestamp()
            {
                return (DateTime.UtcNow.Ticks - offsetDateTimeTicks) / 10000L;
            }
            private static long GetNextTimestamp(long lastTimestamp)
            {
                long timestamp = GetTimestamp();
                int count = 0;
                while (timestamp <= lastTimestamp)
                {
                    count++;
                    if (count > 20)
                        throw new Exception("机器的时间可能不对");
                    else if (count > 5)
                        Thread.Sleep(1);
                    timestamp = GetTimestamp();
                }
                return timestamp;
            }
            public long Generator()
            {
                bool gotLock = false;
                try
                {
                    _lock.TryEnter(ref gotLock);

                    long timestamp = GetTimestamp();
                    if (lastTimestamp == timestamp)
                    {
                        sequence = (sequence + 1) & sequenceMask;
                        if (sequence == 0)
                        {
                            timestamp = GetNextTimestamp(lastTimestamp);
                        }
                    }
                    else
                    {
                        sequence = 0;
                    }
                    if (timestamp < lastTimestamp)
                    {
                        throw new Exception("时间戳比上一次生成ID时时间戳还小，可能系统时钟异常");
                    }
                    lastTimestamp = timestamp;
                    long Id = ((timestamp - twepoch) << (int)timestampLeftShift)
                        | (dataCenterId << (int)dataCenterIdShift)
                        | (machineId << (int)machineIdShift)
                        | sequence;
                    return Id;
                }
                finally
                {
                    if (gotLock)
                        _lock.Exit(false);
                }
            }

        }
    }

    public static class IdGeneratorSetting
    {
        private static readonly Mutex mutex = null;
        private static readonly object locker = new object();
        public static readonly int MachineId = 1;
        static IdGeneratorSetting()
        {
            //解决同一台机器下机器编码问题
            lock (locker)
            {
                while (true)
                {
                    try
                    {
                        mutex = new Mutex(true, "MACHINEID-" + MachineId, out var flag);
                        if (flag) break;
                    }
#pragma warning disable 0168
                    catch (UnauthorizedAccessException e)
                    {
                    }
#pragma warning restore 0168
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    MachineId = System.Threading.Interlocked.Increment(ref MachineId);
                }
            }
        }
    }
}

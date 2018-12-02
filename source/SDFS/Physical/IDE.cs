﻿using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.HAL.BlockDevice;

namespace SDFS.Physical
{
    public class IDE
    {
        /// <summary>
		/// List of ATA IDE Devices
		/// </summary>
		public static List<IDE> Devices
        {
            get
            {
                List<IDE> devs = new List<IDE>();
                for (int i = 0; i < BlockDevice.Devices.Count; i++)
                {
                    if (BlockDevice.Devices[i] is AtaPio)
                    {
                        IDE device = new IDE((AtaPio)BlockDevice.Devices[i]);
                        devs.Add(device);
                    }
                }
                return devs;
            }
        }

        /// <summary>
        /// Retrieves this IDE device's size in Megabytes
        /// </summary>
        public UInt32 Size
        {
            get
            {
                return (uint)(((this.BlockCount * this.BlockSize) / 1024) / 1024) + 1;
            }
        }

        /// <summary>
        /// This object's IDE BlockDevice (ATA hard disk drive)
        /// </summary>
        private BlockDevice blockDevice;

        /// <summary>
        /// Constructor for IDE 
        /// </summary>
        /// <param name="Device"></param>
        public IDE(AtaPio Device)
        {

        }

        /// <summary>
        /// Returns this IDE device's block count
        /// </summary>
        public UInt64 BlockCount
        {
            get
            {
                return this.blockDevice.BlockCount;
            }
        }

        /// <summary>
        /// Returns this IDE device's block size
        /// </summary>
        public UInt64 BlockSize
        {
            get
            {
                return this.blockDevice.BlockSize;
            }
        }

        /// <summary>
		/// Retrieves a new byte array with a length of (num * BlockSize)
		/// </summary>
		/// <param name="num"></param>
		/// <returns>New byte[] Array</returns>
		public Byte[] NewBlockArray(uint num)
        {
            return blockDevice.NewBlockArray(num);
        }

        /// <summary>
		/// Reads the specified amount of blocks from the BlockDevice
		/// </summary>
		/// <param name="aBlockNo">Start block number</param>
		/// <param name="aBlockCount">Number of blocks</param>
		/// <param name="aData">Buffer to write to</param>
		public void ReadBlock(ulong aBlockNo, uint aBlockCount, byte[] aData)
        {
            blockDevice.ReadBlock(aBlockNo, aBlockCount, aData);
        }

        /// <summary>
		/// Writes the specified byte[] array to the specified block and number of blocks
		/// </summary>
		/// <param name="aBlockNo">Start block number</param>
		/// <param name="aBlockCount">Number of blocks</param>
		/// <param name="aData">Buffer to write to</param>
		public void WriteBlock(ulong aBlockNo, uint aBlockCount, byte[] aData)
        {
            blockDevice.WriteBlock(aBlockNo, aBlockCount, aData);
        }
    }
}

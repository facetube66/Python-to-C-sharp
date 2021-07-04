using System;
using System.Collections.Generic;
using HashLib;

namespace PyToC
{
    static class Globals
    {
        public static int WORD_BYTES;            //# bytes in word
        public static int DATASET_BYTES_INIT;        //# bytes in dataset at genesis
        public static int DATASET_BYTES_GROWTH;      //# dataset growth per epoch
        public static int CACHE_BYTES_INIT;          //# bytes in cache at genesis
        public static int CACHE_BYTES_GROWTH;        //# cache growth per epoch
        public static int CACHE_MULTIPLIER;             //# Size of the DAG relative to the cache
        public static int EPOCH_LENGTH;              //# blocks per epoch
        public static int MIX_BYTES;                   //# width of mix
        public static int HASH_BYTES;                   //# hash length in bytes
        public static int DATASET_PARENTS;             //# number of parents of each dataset element
        public static int CACHE_ROUNDS;                  //# number of rounds in cache production
        public static int ACCESSES;                     //# number of accesses in hashimoto loop  

        public static int get_cache_size(int block_number)
        {
            int sz;
            sz = CACHE_BYTES_INIT + CACHE_BYTES_GROWTH * (block_number / EPOCH_LENGTH);
            sz -= HASH_BYTES;
            while (!isprime(sz / HASH_BYTES))
                sz -= 2 * HASH_BYTES;
            
            return sz;
        }

        public static int get_full_size(int block_number)
        {
            int sz = DATASET_BYTES_INIT + DATASET_BYTES_GROWTH * (block_number / EPOCH_LENGTH);
            sz -= MIX_BYTES;
            while (!isprime(sz / MIX_BYTES))
                sz -= 2 * MIX_BYTES;

            return sz;
        }

        public static List<string> mkcache(int cache_size, int seed)
        {
            int n = cache_size;
            var sha3512 = HashFactory.Crypto.SHA3.CreateKeccak512();

            List<string> hash = new List<string>();
            var tempHash = sha3512.ComputeInt(seed);
            hash.Add(tempHash.ToString().ToLower().Replace("-", ""));

            for (int i = 0; i < n; i++)
            {
                tempHash = sha3512.ComputeString(hash[hash.Count - 1]);
                hash.Add(tempHash.ToString().ToLower().Replace("-", ""));
            }

            for (int i = 0; i < CACHE_ROUNDS; i++)
                for (int j = 0; j < n; j++)
                {
                    int v = Int16.Parse(hash[j]) % n;
                    //tempHash = 
                    //hash[j] = 
                }
            return hash;
        }

        public static bool isprime(int index)
        {
            int i, v = 1;
            int a = 1;
            // iterate through 2 to a/2
            for (i = 2; i <= a / 2; i++)
            {
                v = a % i;
                // if remainder is zero, the number is not prime
                if (v == 0)
                    break;
            }
            if (v == 0 || a == 1)
                return true;
            else
                return false;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Globals.WORD_BYTES = 4;                    //# bytes in word
            double dbi = Math.Pow(2, 30);
            double dbg = Math.Pow(2, 23);
            double cbi = Math.Pow(2, 24);
            double cbg = Math.Pow(2, 17);
            Globals.DATASET_BYTES_INIT = Convert.ToInt32(dbi);        //# bytes in dataset at genesis
            Globals.DATASET_BYTES_GROWTH = Convert.ToInt32(dbg);      //# dataset growth per epoch
            Globals.CACHE_BYTES_INIT = Convert.ToInt32(cbi);          //# bytes in cache at genesis
            Globals.CACHE_BYTES_GROWTH = Convert.ToInt32(cbg);      //# cache growth per epoch
            Globals.CACHE_MULTIPLIER = 1024;            //# Size of the DAG relative to the cache
            Globals.EPOCH_LENGTH = 30000;             //# blocks per epoch
            Globals.MIX_BYTES = 128;                  //# width of mix
            Globals.HASH_BYTES = 64;                 //# hash length in bytes
            Globals.DATASET_PARENTS = 256;          //# number of parents of each dataset element
            Globals.CACHE_ROUNDS = 3;                 //# number of rounds in cache production
            Globals.ACCESSES = 64;                  //# number of accesses in hashimoto loop

            //int result = Globals.get_cache_size(10);
            //int result = Globals.get_full_size(10);
            Console.WriteLine("Result");
        }
    }
}

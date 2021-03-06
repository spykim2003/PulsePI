﻿using System;
using System.Linq;
using System.Threading.Tasks;
using PulsePI.DataAccess.DaoInterfaces;
using PulsePI.DataContracts;
using PulsePI.Exceptions;
using PulsePI.Models;
using Microsoft.EntityFrameworkCore;
using PulsePI.MessageContracts;
using System.Collections.Generic;

namespace PulsePI.DataAccess
{
    public class BiometricDataDao : IBiometricDataDao
    {
        private PulsePiDBContext _context;

        public BiometricDataDao(PulsePiDBContext context)
        {
            _context = context;
        }
        
        public async Task CreateBiometricData(Biometric b, string username)
        {
            try
            {
                Account acc = await _context.accounts.Where(x => x.username == username).FirstOrDefaultAsync();
                if (acc == null) throw new InvalidOperationException("There is no account matching the username");

                b.accountId = acc.Id;
                b.account = acc;

                //Add the record to the DB and save 
                _context.biometrics.Add(b);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new CustomException("Database error while trying to create biometric data " + e);
            }
        }

        public async Task<Biometric> GetMostRecentRecord(UsernameData data)
        {
            Account acc = await _context.accounts.Where(x => x.username == data.username).FirstOrDefaultAsync();
            if (acc == null) throw new CustomException("Account doesn't exist");

            List<Biometric> list = null;
            try
            {
                list = await _context.biometrics.Where(x => x.accountId == acc.Id).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new CustomException("Error getting biometric record", ex);
            }
            return list.OrderByDescending(x => x.Date).FirstOrDefault();
        }

        public async Task<Biometric> GetBiometricData(UsernameData data)
        {
            Account acc = await _context.accounts.Where(x => x.username == data.username).FirstOrDefaultAsync();
            if (acc == null) throw new CustomException("Account doesn't exist");

            List<Biometric> b = null;
            try
            {
                b = await _context.biometrics.Where(x => x.accountId == acc.Id).ToListAsync();
            }
            catch(Exception e)
            {
                throw new CustomException("Error getting biometric data" + e);
            }
            return b.OrderByDescending(x => x.Date).FirstOrDefault();          
        }

    }

}



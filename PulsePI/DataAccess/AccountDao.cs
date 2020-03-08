﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PulsePI.DataAccess.DaoInterfaces;
using PulsePI.DataContracts;
using PulsePI.Exceptions;
using PulsePI.MessageContracts;
using PulsePI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PulsePI.DataAccess
{
    public class AccountDao : IAccountDao
    {
        public async Task<LoginMessage> Login(string username, string password)
        {
            using(var context = new PulsePiDBContext())
            {
                Account acc;
                try
                {
                    acc = await context.accounts.Where(x => (x.username == username) &&
                    (x.password == password)).FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    throw new CustomException("Database error at login", e);
                }

                if (acc == null) throw new CustomException("Account not found");

                return new LoginMessage(acc.username, acc.firstName, acc.lastName,
                    acc.middleName, acc.birthDate, acc.avatarUrl, acc.email);
            }
  
        }

        public async Task<CreateAccountMessage> CreateAccount(string u, string p, string f, string l)
        {
            using(var context = new PulsePiDBContext())
            {
                Account acc = await context.accounts.Where(x => (x.username == u) &&
                   (x.password == p)).FirstOrDefaultAsync();
                if (acc != null) throw new CustomException("Account already exists");

                acc = new Account()
                {
                    username = u,
                    password = p,
                    firstName = f,
                    lastName = l
                };

                try
                {
                    context.accounts.Attach(acc);
                }
                catch(Exception e)
                {
                    throw new CustomException("Error creating account", e);
                }
                return new CreateAccountMessage();
                
            }
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileValidator
{
    class FileValidator
    {
        private Dictionary<int, IValidator> validators;  //ColumnIndex, Validator

        private LogFile logFile;

        public FileValidator(Dictionary<int, IValidator> aValidators, string errors_file)
        {
            validators = aValidators;

            logFile = new LogFile(errors_file);
        }

        public void ValidateFile(string file)
        {
            try
            {
                string errorText = ""; //for logFile

                string line;

                using (StreamReader sr = new StreamReader(file))
                {
                    int lineIndex = 0;

                    while ((line = sr.ReadLine()) != null)
                    {
                        lineIndex++;

                        if (!validateLine(line, errorText))
                        {
                            logFile.WriteLine("Error with file: " + file + " on line: " + lineIndex + " " + errorText);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logFile.WriteLine("The file could not be read: " + file);
                logFile.WriteLine(e.Message);
            }
        }
 
        private Boolean validateLine(string line, string errorText)
        {
            string[] fields = line.Split(','); //this could be pipe

            foreach (var pair in validators)
            {
                string field = fields[pair.Key];

                IValidator validator = pair.Value;

                if (!validator.ValidateField(field, errorText))
                {
                    return false;
                }
            }
            return true;
        }
    }
}

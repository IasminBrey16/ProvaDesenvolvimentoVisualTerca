 using System;
using Prova.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Prova
{
    public class Calcular 
    {
        public static double SB(int ht, double vh) 
        {
            return ht * vh;
        }

        public static double IR(double sb) 
        {
            if(sb < 1903.98){
                return 0;
            }
            else if(sb > 1903.98 && sb < 2826.65){
                return 142.80;
            }
            else if(sb > 2826.65 && sb < 3751.06){
                return 354.80;
            }
            else if(sb > 3751.06 && sb < 4664.68){
                return 636.13;
            }
            else {
                return 869.36;
            }
        }
        public static double INSS(double sb) 
        {
            if(sb < 1693.72){
                return sb * 0.08;
            }
            else if(sb > 1693.73 && sb < 2822.90){
                return sb * 0.09;
            }
            else if(sb > 2822.91 && sb < 5645.80){
                return sb * 0.011;
            }
            else {
                return 621.03;
            }
        }
        public static double FGTS(double sb) 
        {
            double aux = sb * 0.08;
            return sb - aux;
        }
        public static double SL(double sb, double ir, double inss) 
        {
            return sb - ir - inss;
        }

    }
}

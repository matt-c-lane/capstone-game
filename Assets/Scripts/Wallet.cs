using UnityEngine;
using System.Collections;
public class Wallet
{
    public static int balance;

    public void Updategold(int payment)
    {
        int temp=balance;
        // Updates total amount of gold in wallet. For subtracting gold, payment will be negative 
        temp+=payment;
        // Check if payment will cause wallet to become negative
        if (temp < 0)
        {
        balance+=payment;
        }
        else
        {
        balance+=payment;
        }
    }

    
}
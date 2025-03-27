using UnityEngine;
using System.Collections;
public class Wallet
{
    public static int balance;

    public bool Updategold(int payment)
    {   // Temporary value to check if you can afford it
        int temp=balance;
        // Updates total amount of gold in wallet. For subtracting gold, payment will be negative 
        temp+=payment;
        // Check if payment will cause wallet to become negative
        if (temp < 0)
        {
        // TODO: Print out a message that says you cannot afford this
        temp+=payment;
        return false;
        }
        else
        {
        balance+=payment;
        return true;
        }
    }

    
}
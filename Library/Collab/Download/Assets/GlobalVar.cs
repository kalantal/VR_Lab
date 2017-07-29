using System;

public static class GlobalVar
{

   public static  int interfaces=0;

    public static void toggleInterface()
    {
        interfaces++;
        if (interfaces == 4) interfaces = 0;
    }
}

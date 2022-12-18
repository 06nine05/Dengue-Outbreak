using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenLink : MonoBehaviour
{
    public void OpenReffLink(){
     Application.OpenURL("https://ddc.moph.go.th/disease_detail.php?d=44");
    }

    public void OpenAnotherReffLink()
    {
        Application.OpenURL("https://www.bangpakok3.com/care_blog/view/200");
    }
    
    public void OpenURL(string link)
    {
      Application.OpenURL("link");
    
    }   

}

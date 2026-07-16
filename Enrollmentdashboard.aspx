<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Enrollmentdashboard.aspx.cs" MasterPageFile="~/Site.master"
    Inherits="Enrollmentdashboard" %>
       <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

<style>
    .modalBackground
{
    background-color:Gray !important; 
    filter:alpha(opacity=50) !important;
    opacity:0.7 !important;
}
</style>
 <style>
        .h22:after {
    content: '';
    display: block;
    position: relative;
    width: 65px;
    border: 4px solid #ed3237;
    margin-top: 2px;
}
.h33:after {
    content: '';
    display: block;
    position: relative;
    width: 66%;
    border: 2px solid #ed3237;
    margin-top: 20px;
    margin-left: 17%;
}
.div-100{
    float: left;
    width: 100%;
    height: 100%;
}
.div-100 a{
    float: left;
    width: 100%;
    height: auto;
    color:#000000;
   
}
.div-100 a h2{ 
   font-size:20px;

   
}

.div-100 a:hover{
    float: left;
    width: 100%;
    height: auto;
    color :#ed3237;
    text-decoration:none;
}
.stretched-link {
   position: inherit;
}
.stretched-link:after {
   position: absolute;
   top: 0;
   right: 0;
   bottom: 0;
   left: 0;
   z-index: 1;
   background-color: transparent;
   content: "";
   pointer-events: auto;
}
.bord-rig {
  width: 33%;
  height: 100%;
  position: relative;
  z-index: 1;
}
.bord-rig:before {
    content: "";
    position: absolute;
    right: 0;
    top: 0%;
    height: 55%;
    width: 2px;
    border-right: 2px solid #5f5e5e;
}
.div-graw{
    width: 70%;
    background-color: #f1f1f1;
    margin-left: 15%;
    border: 1px solid #efefec;
    float: left;
}
     .div-graw span {
         top: 12px;
         position: relative;
         font-weight: bold;
     }

      .div-graw h3 {
        margin-top: 10px;
    margin-bottom: 20px;
     }

.greid-v{
    display:grid;
    grid-template-columns:auto auto auto auto auto;
    gap:15px;
    text-align:center;
}

@media (min-width: 0px) and (max-width: 767px) {
.bord-rig {
  width: 100%;
}
.div-graw{
    width: 90%;
}


.bord-rig:before {
    content: "";
    position: absolute;
    right: 0;
    top: 0%;
    height: 55%;
    width: 0px;
    border-right: 0px solid #5f5e5e;
}
}
    </style>
    <!--Right Sidebar-->
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <div class="row" >
     
            <div class="row">             
                                
               <div class="panel-body panel-body-new">
                                
                          <div class="row">
        <div class="col-sm-12">
            <h1 class="h22" style="font-weight: bold;">Enrollment</h1>
        </div>
    </div>
                              <div class="row">
        <div class="container">
            <div class="row text-center" style="margin-top: 20px;">
                <div class="col-sm-12">
                    <div class="greid-v">
                        
                             <div class="div-100">
                                 <a href="FrmEnrollmentDuplicateMatching.aspx?rid=2" accesskey="2"  class="stretched-link1">
                                <div id="DivBlock" runat="server">
                             <img src="Images/duplicate_matching.png" style="height: 80px;">
                                <h2>Pending <br>  Duplicate Matching</h2>
                                        
                            </div>

                                   </a>
                                  <div class="div-graw" id="DivBlockCount" runat="server">
                                       <a href="FrmEnrollmentDuplicateMatching.aspx?rid=2" accesskey="2"  class="stretched9-link">
                         <asp:Label Font-Bold="true" Font-Size="Large" style="font-weight: bold;margin-top: 30px;" runat="server" ID="lblDuplicate" Text="5234"></asp:Label> 
           
                            <h3 class="h33" style="margin-top: 10px;margin-bottom: 30px;"></h3>
                        </a>
                    </div>
                        </div>
                          <div class="div-100">
                              <a href="FrmEnrollmentBlockWiseMatching.aspx?rid=2" accesskey="2"  class="stretched-link1">
                              <div  id="divManual" runat="server">
                             <img src="Images/manual_matching.png" style="height: 80px;" />
                                <h2>Pending <br>  Matching</h2>
                                        
                            </div>
                              </a>
                              
                    <div class="div-graw"  id="divManual1" runat="server">
                          <a href="FrmEnrollmentBlockWiseMatching.aspx?rid=2" accesskey="2"  class="stretchejd-link">
                         <asp:Label Font-Bold="true" Font-Size="Large" style="font-weight: bold;margin-top: 30px;" runat="server" ID="Label2" Text="5234"></asp:Label> 
           
                            <h3 class="h33" style="margin-top: 10px;margin-bottom: 30px;"></h3>
                       </a>
                    </div>
                         </div>
                       

                                      <div class="div-100">
                                            <a href="FrmEnrollmentBlockWiseGenration.aspx?rid=2" accesskey="2"  class="stjretched-link1">
                                            <div id="divG" runat="server">
                                                  <img src="Images/generation.png" style="height: 80px;">
                                <h2>Pending <br>Seal-Sign Generation</h2>
                              
                            
                            </div>   </a>   <div class="div-graw" id="divG1" runat="server">
                                <a href="FrmEnrollmentBlockWiseGenration.aspx?rid=2" accesskey="2"  class="stretcjhed-link">
                        <asp:Label Font-Bold="true" Font-Size="Large" style="font-weight: bold;margin-top: 30px;" runat="server" ID="LblA" Text="5234"></asp:Label> 
                             <h3 class="h33" style="margin-top: 10px;margin-bottom: 30px;"></h3>
                        </a>
                    </div>
                         </div>
                      
                             
                             <div class="div-100">
                          <a href="FrmEnrollmentBlockWise.aspx?rid=1" accesskey="2"  class="stretched-jlink1">
                                    <div id="dvValdation" runat="server"> 
                        <img src="Images/validation.png" style="height: 80px;">
                        <h2>Pending <br>Seal-Sign Validation</h2>
                           
                    </div> </a>
                                    <div class="div-graw" id="dvValdation1" runat="server">
                                          <a href="FrmEnrollmentBlockWise.aspx?rid=1" accesskey="2"  class="stretched-ljink">
                         <asp:Label Font-Bold="true" Font-Size="Large" style="font-weight: bold;margin-top: 30px;" runat="server" ID="Label1" Text="5234"></asp:Label> 
           
                            <h3 class="h33" style="margin-top: 10px;margin-bottom: 30px;"></h3>
                       </a>
                    </div>

                             </div>
                        
                             <div class="div-100">
                                      <a href="FrmENrollmentSC.aspx?rid=1" accesskey="2"  class="stretched-ljink1">
                                  <div  id="dvcv" runat="server">
                        <img src="Images/img-ed.png" style="height: 80px;">
                        <h2>Pending <br>Enrollment Course Correction</h2>
                       
                          </div>        
                    <div class="div-graw" id="dvcv1" runat="server">
                         <a href="FrmENrollmentSC.aspx?rid=1" accesskey="2"  class="stretched-lijnk">
                         <asp:Label Font-Bold="true" Font-Size="Large" style="font-weight: bold;margin-top: 30px;" runat="server" ID="Label3" Text="5234"></asp:Label> 
           
                            <h3 class="h33" style="margin-top: 10px;margin-bottom: 30px;"></h3>
                        </a>
                    </div> </a>
                                 </div>
                         

                    </div>
             
        
             
        
        
        
            </div>
        </div>
                </div>

               </div>
                               
                                          
 </div>
       
</asp:Content>


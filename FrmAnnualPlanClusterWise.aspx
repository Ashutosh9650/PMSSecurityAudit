<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmAnnualPlanClusterWise.aspx.cs" Inherits="FrmAnnualPlanClusterWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>

        .btnStyle {
            border: 1px solid #ccc;
            margin-bottom: 7px;
            margin-right: 16px;
        }
        .padd {
    padding-left: 15px;
    padding-right: 15px;
}
        table tr td, table tr th  {
    padding: 8px;
}
    </style>
 <script type="text/javascript">
     function onEvent1(th) {
         var dd = $("#" + th).val();
         if (dd == 1 || dd == 2 || dd == 3 || dd == 0 | dd == "") {

         }
         else {
             alert('Please enter 1 for District Level Training  2 for Block Level training  and  3 for Cluster Level Training.');
             $("#" + th).val('');
             return false;
         }
     }
     function onEvent(th) {
         var idx = -1, stidx = 0, lstidx = 0;
         var v1 = 0;
         var T1 = 0;
         var Target = 0;
         var k1 = 0;
         var k2 = 0;
         var Start = 0;
         var End = 0;
         var MaxVal = 0;
         var TarVal = 0;
         var txt1idx = 0;
         var Type = $('.clsType').val();
         var uh = 0; var TxtName = "";
         $(th).closest('tr').find('td').each(function (i) {

             if (k1 == 0) {
                 var R = $(this).find("span[class='S']").text();
                 k1 = 1;
                 Start = R;
                 var R1 = $(this).find("span[class='E']").text();
                 k2 = 1;
                 End = R1;
                 var M1 = $(this).find("span[class='M']").text();
                 MaxVal = M1;
                 var K1 = $(this).find("span[class='K']").text();
                 TarVal = K1;
             }


          
             if (Type == "1") {

                 stidx = Start;
                 lstidx = End;

             }
             else if (Type == "2") {
                 stidx = Start;
                 lstidx = End;
                 if (MaxVal == 13 || MaxVal == 14 || MaxVal == 15) {
                     stidx = 0;
                     lstidx = 12;


                 }
                 if ($(this).find('span').html() == "7-14 Years OOSG Enrolment Goal(Ops)") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "6 Years OOSG Goal") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "7-14 Years OOSG Goal") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "15-18 Years OOSG Goal") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "#GKP Plus Schools") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "#GKP Plus Beneficiaries") {
                     TxtName = $(this).find('span').html();
                 }
                
             }
             else if (Type == "3") {

                 stidx = Start;
                 lstidx = End;

                 if (MaxVal == 5 || MaxVal == 3 || MaxVal == 2 || MaxVal == 6) {
                     stidx = 0;
                     lstidx = 12;
                 }

             }
             if (i > 0) {
                
                 var jui = 0;
                
                 //                  


                 if (idx >= stidx && idx <= lstidx && !isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                     txt1idx = i;

                     v1 += parseFloat($(this).find("input[type='text']").val());
                     if (Type == "1") {
                       
                         if (MaxVal == 3) {

                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                
                               
                                 if (hh > 999) {
                                     $(th).val('0');
                                     alert('Entry allowed  max 3 Digit numbers!!.');
                                     return false;
                                 }
                             }
                         }
                         else if (MaxVal == 4) {

                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;
                                 var hh = parseFloat($(this).find("input[type='text']").val());

                                 if (hh > 9999) {
                                     $(th).val('0');
                                     alert('Entry allowed  max 4 Digit numbers!!.');
                                     return false;
                                 }
                             }
                         }
                         
                        
                     }
                     else if (Type == "2") {
                        
                         if (TxtName == "7-14 Years OOSG Enrolment Goal(Ops)") {
                            
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 T1 += parseFloat($(this).find("input[type='text']").val());
                                 if (parseFloat(Target) == 0) {
                                     Target = $(this).find("span[class='K']").text();
                                 }
                                /* var HK3 = ppneedjjValue("7-14 Years OOSG Enrolment Goal(Ops)", 0, 0, '');*/
                               
                                 if (Target < T1) {
                                     $(th).val('0');
                                     alert('The Enrolment universe is ' + Target + '. You cannot enter the enrollment goal more than ' + Target + '!!');
                                     return false;
                                 }


                             }
                         }
                         if (TxtName == "#GKP Plus Schools") {
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                 
                                 var gg = ppneedBlank("#GKP Plus Beneficiaries", 0, 0, '');
                                 if (hh>6) {
                                     $(th).val('0');
                                     alert('Maximum value allowed is 6 !!');
                                     return false;
                                 }


                             }
                         }
                         if (TxtName == "#GKP Plus Beneficiaries") {
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                 var M1 = ppneedjjValue("#GKP Plus Schools", 0, 0, '');
                                 var tu = parseFloat(M1) * 100;
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                 if (M1 == 0) {
                                     $(th).val('0');
                                     alert('Please enter #GKP Plus Schools Frist !!');
                                     return false;
                                 }
                                 if (tu < hh)
                                 {
                                     $(th).val('0');
                                     alert('Maximum value allowed  ' + tu + '!!');
                                     return false;
                                 }
                                


                             }
                         }
                        
                         if (TxtName == "7-14 Years OOSG Goal") {
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                 var K1 = $(this).find("span[class='K']").text();

                                 if (K1 < hh) {
                                     $(th).val('0');
                                     alert('The Enrolment universe is ' + K1 + '. You cannot enter the enrollment goal more than ' + K1 + '!!');
                                     return false;
                                 }


                             }
                         }
                         if (TxtName == "15-18 Years OOSG Goal") {
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                 var K1 = $(this).find("span[class='K']").text();
                                 if (K1 < hh) {
                                     $(th).val('0');
                                     alert('The Enrolment universe is ' + K1 + '. You cannot enter the enrollment goal more than ' + K1 + '!!');
                                     return false;
                                 }


                             }
                         }
                         if (TxtName == "7-14 Years OOSB") {
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 var hh = parseFloat($(this).find("input[type='text']").val());
                                 var K1 = $(this).find("span[class='K']").text();
                                 if (K1 <= hh) {
                                     $(th).val('0');
                                     alert('The Enrolment universe is ' + K1 + '. You cannot enter the enrollment goal more than ' + K1 + '!!');
                                     return false;
                                 }


                             }
                         }
                         if (MaxVal == 1) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 var hh = parseFloat($(this).find("input[type='text']").val());

                                 if (hh > 9) {
                                     $(th).val('0');
                                     alert('Entry allowed  max 1 Digit numbers!!.');
                                     return false;
                                 }

                             }
                         }
                         else if (MaxVal == 2)
                         {

                             var hh = parseFloat($(this).find("input[type='text']").val());

                             if (hh > 99)
                             {
                                 $(th).val('0');
                                 alert('Entry allowed  max 2 Digit numbers!!.');
                                 return false;
                             }

                             
                         }
                         else if (MaxVal == 3) {

                             var hh = parseFloat($(this).find("input[type='text']").val());

                             if (hh > 999) {
                                 $(th).val('0');
                                 alert('Entry allowed  max 3 Digit numbers!!.');
                                 return false;
                             }


                         }

                         else if (MaxVal == 4) {

                             var hh = parseFloat($(this).find("input[type='text']").val());

                             if (hh > 9999) {
                                 $(th).val('0');
                                 alert('Entry allowed  max 4 Digit numbers!!.');
                                 return false;
                             }


                         }



                     }
                    
                 }
             }
             idx++;
         });
     }

    
     function ppneedjjValue(txt, stidx, val, SumValue) {
         var idx = 0;
         var RVal = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "#GKP Plus Schools") {

                         if (i >0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))
                             if (RVal <=0)
                             {
                                 RVal += parseFloat($(this).find("input[type='text']").val());
                                
                           }
                         }
                     }

                     idx++;
                 });
             }
         });
         return RVal;
     }

     function ppneedBlank(txt, stidx, val, SumValue) {
         var idx = 0;
         var RVal = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "#GKP Plus Beneficiaries") {

                         if (i > 0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                 $(this).find("input[type='text']").val('');
                             

                         }
                     }

                     idx++;
                 });
             }
         });

     }
     function ppneedjjValueComine(txt, stidx, val, SumValue) {
         var idx = 0;
         var RVal = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Need- Enrolment") 
                     {

                         if (i > 0)
                          {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                 if (RVal <= 0) {
                                     RVal = $(this).find("input[type='text']").val();
                                 }

                         }
                         }
                         if (txt == "TB Need- Enrolment+Learning") {

                             if (i > 0) {
                                 if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                     if (RVal <= 0) {
                                         RVal = $(this).find("input[type='text']").val();
                                     }

                             }
                         }

                     if (txt == "TB Need- Learning") {

                         if (i > 0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                 if (RVal <= 0) {
                                     RVal = $(this).find("input[type='text']").val();
                                 }

                         }
                     }

                     idx++;
                 });
             }
         });
         return RVal;
     }


     function LSGTotal(txt, stidx, val, SumValue) {
         var idx = 0;
         var RVal = 0;
         var Total = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "LSE Sessions") {

                         if (i > 0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                
                                     RVal += parseFloat($(this).find("input[type='text']").val());

                                     Total = parseFloat(Total) + parseFloat(RVal);
                            
                            

                         }
                     }
                     
                     idx++;
                 });
             }
         });
         return RVal;
     }

     function ppneedjjLearningValue(txt, stidx, val, SumValue) {
         var idx = 0;
         var RVal = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Need- Learning") {

                         if (i > 0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                 if (RVal <= 0) {
                                     RVal = $(this).find("input[type='text']").val();
                                 }

                         }
                     }
                     if (txt == "TB Need- Enrolment+Learning") {

                         if (i > 0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))

                                 if (RVal <= 0) {
                                     RVal = $(this).find("input[type='text']").val();
                                 }

                         }
                     }
                     idx++;
                 });
             }
         });
         return RVal;
     }

     function ppOnly(txt, stidx, val, SumValue) {
         var idx = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Handhold- Enrolment") {

                         if (i > 0) {
                             if (idx >= stidx && val == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');

                             } else if (idx >= stidx && val > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                                 $(this).find("input[type='text']").val(val * 2);

                             }
                         }
                     }
                  
                    
                     idx++;
                 });
             }
         });
     }
     function ppOnly1(txt, stidx, val, SumValue) {
         var idx = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Handhold- Learning") {

                         if (i > 0) {
                             if (idx >= stidx && val == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');

                             } else if (idx >= stidx && val > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                                 $(this).find("input[type='text']").val(val * 2);

                             }
                         }
                     }


                     idx++;
                 });
             }
         });
     }

     function ppOnly2(txt, stidx, val, SumValue) {
         var idx = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Handhold- Enrolment + Learning") {

                         if (i > 0) {
                             if (idx >= stidx && val == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');

                             } else if (idx >= stidx && val > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                                 $(this).find("input[type='text']").val(val * 2);

                             }
                         }
                     }


                     idx++;
                 });
             }
         });
     }
     function ppneed(txt, stidx, val, SumValue) {
         var idx = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Handhold- Enrolment") {

                         if (i > 0) {
                             if (idx >= stidx && val == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');
                             
                             } else if (idx >= stidx && val > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                                 $(this).find("input[type='text']").val(val * 2);
                                
                             }
                         }
                     }
                     if (txt == "TB Handhold- Learning") {

                         if (i > 0) {
                             if (idx >= stidx && val == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');
                             } else if (idx >= stidx && val > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                                 $(this).find("input[type='text']").val(val * 2);
                             }
                         }
                     }
                     if (txt == "TB Handhold- Enrolment + Learning") {

                         if (i > 0) {
                             if (idx >= stidx && val == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');
                             } else if (idx >= stidx && val > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                                 $(this).find("input[type='text']").val(val * 2);
                             }
                         }
                     }

                     idx++;
                 });
             }
         });
     }
     function ppDDisabl(txt, stidx, val, SumValue) {
  
         var idx = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                    
                     if (txt == "TB Handhold- Enrolment + Learning") {
                      
                         if (i == 1) {
                           
                             if (idx >= i && val > 0) {
                                
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                             }
                             else if (val == 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                             }
                         }
                     }
                     if (txt == "TB Need- Enrolment") {
                         if (i == 1) {
                             if (idx >= i && val > 0) {

                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                             }
                             else if (val == 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                             }
                         }
                     }
                     if (txt == "TB Need- Learning") {
                         if (i == 1) {
                             if (idx >= i && val > 0) {

                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                             }
                             else if (val == 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                             }
                         }
                     }



                     idx++;
                 });
             }
         });
     }

     function pp(txt, stidx, val, SumValue) {
         var idx = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {
                     if (txt == "Learning Endline for GKP") {
                         if (i == 12) {
                             if (idx >= i && SumValue > 0) {

                                 $(this).find("input[type='text']").val(1);
                             }
                             else if (SumValue == 0) {
                                 $(this).find("input[type='text']").val(0);
                             }
                         }
                     }
                     //                        else if (txt == "GKP L0" || txt == "GKP L1" || txt == "GKP L2" || txt == "GKP L3") {

                     //                            if (i > 0) {
                     //                                if (idx >= stidx && val == 0 && SumValue == 0) {
                     //                                    $(this).find("input[type='text']").attr("disabled", "disabled");
                     //                                    $(this).find("input[type='text']").val('0');
                     //                                } else if (idx >= stidx && val > 0 && SumValue > 0) {
                     //                                    $(this).find("input[type='text']").removeAttr("disabled");
                     //                                }

                     //                            }
                     //                        }
                     else if (txt == "LSE Sessions") {
                         if (i > 0) {
                             if (idx >= stidx && val == 0 && SumValue == 0) {
                                 $(this).find("input[type='text']").attr("disabled", "disabled");
                                 $(this).find("input[type='text']").val('0');
                             } else if (idx >= stidx && val > 0 && SumValue > 0) {
                                 $(this).find("input[type='text']").removeAttr("disabled");
                             }
                         }
                     }
                     else if (i > 0) {
                         if (idx >= stidx) {
                             $(this).find("input[type='text']").val(val * 2);
                         }
                     }
                     idx++;
                 });
             }
         });
     }

     function isNumberKey(txt, evt) {
         var charCode = (evt.which) ? evt.which : evt.keyCode;
         if (charCode == 46 && charCode == 127) {
             if (txt.value.indexOf('.') === 1) {
                 return true;
             }
             else {
                 return false;
             }
         }
         else {
             if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                 return false;
             }
             else {
                 return true;
             }
         }
     }
 </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel runat="server" ID="Upnl">
        <ContentTemplate>
            <div class="container-fluid" style="margin-top: 0px;">
                <div class="row" >
                    <div class="col-lg-2 col-md-2 col-sm-3 clsmain" style="padding-right: 0px;" runat="server" visible="false">
                        <div class="thumbnail" style="min-height: 750px; width: 228px;">
                            <div style="padding-top: 3px;">
                                <%--<span style="float:left"> <asp:Label ID="lblsearch" runat="server" Text="Search:" ForeColor="Black"></asp:Label></span>--%>
                                <span style="float: right; padding-right: 1px;">
                                    <asp:TextBox ID="txtSearchName" Visible="false" runat="server" OnTextChanged="txtSearchName_Click"
                                        AutoPostBack="true" CssClass="form-control col-lg-1"></asp:TextBox></span>
                            </div>
                            <div style="overflow: auto; margin-top: 35px; height: 750px;">
                                <asp:GridView ID="GVMain" runat="server" Width="100%" AllowPaging="true" PageSize="40"
                                    BorderStyle="None" DataKeyNames="VillageCode,DISECode,RowNo,SchoolLevel,BAlVal,GKP,GKPLevel,ManagementType"
                                    GridLines="None" AutoGenerateColumns="false" OnRowCommand="GVMain_OnRowCommand"
                                    OnPageIndexChanging="GV_Project_PageIndexChanging">
                                    <EmptyDataTemplate>
                                        <div style="font-family: Arial; font-size: 12px; font-weight: bold; color: Red;">
                                            Data not found
                                        </div>
                                    </EmptyDataTemplate>
                                    <FooterStyle CssClass="FooterStyle" />
                                    <HeaderStyle BackColor="#C1C1C1" ForeColor="White" Height="44px" />
                                    <RowStyle HorizontalAlign="Left" />
                                    <PagerStyle CssClass="paging" />
                                    <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                                    <AlternatingRowStyle BackColor="#f1f1f1" />
                                    <%-- <SelectedRowStyle BackColor="#fad669" Font-Bold="True" ForeColor="WhiteSmoke" />
                    <AlternatingRowStyle BackColor="#f1f1f1" />--%>
                                    <Columns>
                                        <asp:ButtonField HeaderText="Village Name " ItemStyle-ForeColor="#333" DataTextField="VillageName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="School Name" ItemStyle-ForeColor="#333" DataTextField="SchoolName"
                                            CommandName="GVUIO">
                                            <ItemStyle CssClass="padding-lef" Height="30px" />
                                            <HeaderStyle CssClass="padding-lef" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12 col-md-12 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading" style="padding-left: 0px;padding-right: 0px;">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    Annual Plan Entry</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 ">
                                                <%--<input type="image" id="ton-new" runat="server" visible="false" class="butt" src="Images/search-not-29.png" title="Search" />--%>
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 0px;
                                                    padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" 
                                                    Style="margin-right: 0px; padding: 0px;" runat="server" />
                                               
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new" style="text-align: left;">
                                                <div class="row marg search-bg" style="padding: 15px 0px 15px 0px;">
                                                    <div class="form-horizontal">
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Year:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlYear" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                                        class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                    Level:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlType" runat="server" OnSelectedIndexChanged="ddlType_SelectedIndexChanged"
                                                                        AutoPostBack="true" CssClass="form-control clsType">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                        <asp:ListItem Text="District Level" Value="1"></asp:ListItem>
                                                                        <asp:ListItem Text="Cluster Level" Value="2"></asp:ListItem>
                                                                        
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                          <div class="col-lg-3 col-md-3 col-sm-3 col-xs-12"  runat="server" id="divSub">
                                                            <div class="form-group" style="margin-bottom: 7px;">
                                                                <label for="email" class="col-sm-3 padd linhei">
                                                                  Entry Type:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlsubType" runat="server"   OnSelectedIndexChanged="ddlSubType_SelectedIndexChanged"
                                                                        AutoPostBack="true"
                                                                     CssClass="form-control clsType">
                                                                   
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    State:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlState" runat="server" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control ">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div class="form-group">
                                                                <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                    District:</label>
                                                                <div class="col-sm-9 padd">
                                                                    <asp:DropDownList ID="ddlDistrict" runat="server" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"
                                                                        AutoPostBack="true" class="form-control " />
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12 ">
                                                            <div runat="server" id="divBlock" style="display: none;">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Block:</label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged"
                                                                            class="form-control " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div runat="server" id="divPhy" style="display: none;">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Cluster:</label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlPanchayat" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPanchayat_SelectedIndexChanged"
                                                                            class="form-control " />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <div runat="server" id="divVill" style="display: none;">
                                                                <div class="form-group">
                                                                    <label for="email" class="col-sm-3 padd linhei" style="padding-top: 2px;">
                                                                        Village:</label>
                                                                    <div class="col-sm-9 padd">
                                                                        <asp:DropDownList ID="ddlVillage" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                                                                            AutoPostBack="true" runat="server" class="form-control " />
                                                                        <span style="width: 5px; float: right; margin: -27px 27px; font-size: 21px;">
                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Display="Dynamic"
                                                                                ValidationGroup="saves" ControlToValidate="ddlVillage" ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
                                                                        </span>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-3 cpl-xs-12">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                           
                                          
                                        </div>
                                    </div>
                                     <div class="row">
                                          <div class="col-sm-12">
                                           <div id="div-show-6new">
                                                <div class=" marg search-bg"  style="float:left;width:100%;">
                                                  <div class="col-sm-12" style="text-align:right; padding:0px">
                                                                                                        
                                                       
                                                            
                                                                    <asp:LinkButton ID="lnkDownload" CssClass="btn btn-link btnStyle" OnClick="AnnalPlanExcel_Hotspot" runat="server">Download Excel</asp:LinkButton>
                                                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="btnStyle" style="display:inline-block"   />   
                                                      <asp:LinkButton ID="LinkButton1" CssClass="btn btn-link btnStyle" OnClick="btnImport_Click" runat="server">Upload</asp:LinkButton>
                                                          
                                                          <asp:Button ID="btnSubmitted" OnClick="btnSubmitted_Click" CssClass="btn btn-link btnStyle" Enabled="false" Text="Submit to DOL" runat="server"></asp:Button>
                                                       <asp:Button ID="btnReject"  OnClick="btnReject_Click" CssClass="btn btn-link btnStyle" Visible="false" runat="server" Text="Reject"></asp:Button>
                                                        <asp:Button ID="btnUnlock"  OnClick="btnUnlock_Click" CssClass="btn btn-link btnStyle" Visible="false" runat="server" Text="Unlock"></asp:Button>
                                                    </div>
                                                    </div>
                                                 </div>
                                              </div>
                                     </div>
                                   
                                    <div class="row">

                                          <div class="col-lg-12">
                                                <asp:Panel ID="pnlMain" Enabled="false" runat="server">
                                                    <div class="row">
                                                    <asp:Label ID="lblMsg" CssClass="pull-right" style="font-size: medium; color: red;margin-right: 469px;"  Visible="false" Text="Please enter no. of participants here" runat="server"></asp:Label>
                                                        <div id="DVEE" runat="server" class="thumbnail clsAnnualPlan" style="float: left;
                                                            width: 100%;">
                                                            <asp:GridView ID="GV_AnnualPlan" Width="100%" ShowFooter="true" runat="server" BorderStyle="None"
                                                                OnRowDataBound="GV_AnnualPlan_OnRowDataBound" GridLines="None" AutoGenerateColumns="false">
                                                                <EmptyDataTemplate>
                                                                </EmptyDataTemplate>
                                                                <FooterStyle CssClass="FooterStyle" />
                                                                <HeaderStyle BackColor="#f5f5f5" ForeColor="Black" Height="25px" />
                                                                <RowStyle HorizontalAlign="Center" />
                                                                <SelectedRowStyle BackColor="#fad669" ForeColor="WhiteSmoke" />
                                                                <AlternatingRowStyle BackColor="#f1f1f1" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Description">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="LblDesc" CssClass="D" Text='<%#Bind("Activity") %>' runat="server"></asp:Label>
                                                                              <asp:Label ID="lblStartMonth" CssClass="S"   style="color: blue;display:none;" Text='<%#Bind("StartMonth") %>'  runat="server"></asp:Label>
                                                                              <asp:Label ID="lblEndMonth" CssClass="E"   style="color: blue;display:none;" Text='<%#Bind("EndMonth") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lblMaxVal" CssClass="M"   style="color: blue;display:none;" Text='<%#Bind("MaxVal") %>' runat="server"></asp:Label>
                                                              
                                                                      
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Training Level">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtTrainingLevel" MaxLength="2"  CssClass="form-control cMay"
                                                                              onchange="return onEvent1(this.id);"   Text='<%#Bind("TrainingLevel") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Quarter 1">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtApr" MaxLength="4" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Q1") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        <asp:Label ID="lblTarget"   CssClass="K"  style="color: blue;display:none;" Text='<%#Bind("Q5") %>' runat="server"></asp:Label>
                                                              
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quarter 2">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMay" MaxLength="4" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Q2") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        <asp:Label ID="lblTahhrget"   CssClass="K"  style="color: blue;display:none;" Text='<%#Bind("Q5") %>' runat="server"></asp:Label>
                                                              
                                                                            
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quarter 3">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJun" MaxLength="4" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Q3") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Quarter 4">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJul" MaxLength="4" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Q4") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
                                    </div>
                                    <!-- /#page-content-wrapper -->
                                </div>
                                <!-- /#wrapper -->
                                <!-- /#wrapper -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
             <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="Hdn_model4"
                PopupControlID="pnlpopup4" CancelControlID="CancelButton2" BackgroundCssClass="modalBackground">
            </cc1:ModalPopupExtender>
            <asp:HiddenField ID="Hdn_model4" runat="server" />
            <asp:Panel ID="pnlpopup4" runat="server" Style="display: none;width:80%">
                <div class="modal-dialog modal-md">
                    <div class="modal-content">
                        <div class="modal-header" style="height: 0px;">
                             <asp:ImageButton ID="CancelButton2" ImageUrl="~/images/close-29.png" runat="server"
                                        Text="Close" ToolTip="Close" Style="border-width:0px;float: none;margin-left: 547px;margin-top: -8px;"></asp:ImageButton>
                          
                        </div>
                        <div class="row" >
                            <div class="row marg search-bg">
                                <div class="col-lg-10 col-md-10 col-sm-10 cpl-xs-12">
                                    <div class="col-lg-8 col-md-8  col-sm-8 cpl-xs-12">
                                        <div class="form-group" style="margin-bottom: 2px;">
                                            <label for="email" class="col-sm-3 padd linhei">
                                                Remarks:</label>
                                            <div class="col-sm-9 padd">
                                               <asp:TextBox ID="txtRemark" runat="server" Width="171%" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                                                 <asp:RequiredFieldValidator ID="ReqTxtDate" runat="server" ControlToValidate="txtRemark"
                                            Display="Dynamic" ErrorMessage="Please Enter Remark for Rejection" Font-Bold="False" Font-Size="9px" ForeColor="Red"
                                            SetFocusOnError="True" ValidationGroup="Savdata"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </div>
                            <div class="col-lg-12 col-md-12 col-sm-12 cpl-xs-12">

                                       <asp:ImageButton ID="ImageButton1" ValidationGroup="Savdata" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsaveReject_Click" 
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
        <Triggers >
             
                                    
              <asp:PostBackTrigger ControlID="lnkDownload" />
                <asp:PostBackTrigger ControlID="LinkButton1" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

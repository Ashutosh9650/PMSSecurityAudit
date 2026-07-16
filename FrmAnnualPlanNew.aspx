<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="FrmAnnualPlanNew.aspx.cs" Inherits="FrmAnnualPlanNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <script type="text/javascript">
     function onEvent(th) {
         var idx = -1, stidx = 0, lstidx = 0;
         var v1 = 0;

         var k1 = 0;
         var k2 = 0;
         var Start = 0;
         var End = 0;
         var MaxVal = 0;
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

                 if ($(this).find('span').html() == "TB Need- Enrolment") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "TB Need- Learning") {
                     TxtName = $(this).find('span').html();
                 }
                 if ($(this).find('span').html() == "TB Need- Enrolment+Learning") {
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
                 if (i == 1 && Type == "2" && TxtName == "TB Need- Enrolment") {
                     if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {

                     }
                     else {

                         if (TxtName == "TB Need- Enrolment") {
                             ppneed("TB Handhold- Enrolment", 0, 0, '');
                         }

                     }
                 }
                 if (i == 1 && Type == "2" && TxtName == "TB Need- Learning") {

                     if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {

                     }
                     else {
                         ppneed("TB Handhold- Learning", 0, 0, '');
                     }
                 }
                 if (i == 1 && Type == "2" && TxtName == "TB Need- Enrolment+Learning") {

                     if (!isNaN(parseFloat($(this).find("input[type='text']").val()))) {

                     }
                     else {
                         ppneed("TB Handhold- Enrolment + Learning", 0, 0, '');
                     }
                 }

                 //                  


                 if (idx >= stidx && idx <= lstidx && !isNaN(parseFloat($(this).find("input[type='text']").val()))) {
                     txt1idx = i;

                     v1 += parseFloat($(this).find("input[type='text']").val());
                     if (Type == "1") {

                         if (MaxVal == 3) {

                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 3) {
                                     $(th).val('0');
                                     alert('Entry allowed in any three months!!.');
                                     return false;
                                 }
                             }
                         }
                         else if (MaxVal == 10) {
                         }
                         else if (MaxVal == 2) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 2) {
                                     $(th).val('0');
                                     alert('Entry allowed in any two months!!.');
                                     return false;
                                 }
                             }
                         }
                         else if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                             uh = uh + 1;

                             if (uh > 1) {
                                 $(th).val('0');
                                 alert('Entry allowed in any one month!!.');
                                 return false;
                             }
                         }
                     }
                     else if (Type == "2") {

                         if (MaxVal == 1) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                         }
                         else if (MaxVal == 2) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 2) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 if (TxtName == "TB Need- Enrolment") {

                                     ppneed("TB Handhold- Enrolment", 0, 0, '');
                                 }
                                 if (TxtName == "TB Need- Learning") {

                                     ppneed("TB Handhold- Learning", 0, 0, '');
                                 }
                                 if (TxtName == "TB Need- Enrolment+Learning") {

                                     ppneed("TB Handhold- Enrolment + Learning", 0, 0, '');
                                 }
                                 alert('Only 1 and 2 can be entered!!');
                                 return false;

                             }
                             else {

                                 var KK = 0;
                                 if (!isNaN(parseFloat(v1))) {
                                     KK = v1;
                                     var M1 = ppneedjjValueComine("TB Need- Enrolment+Learning", 0, 0, '');
                                     if (TxtName == "TB Need- Enrolment") {

                                         if (KK <= 2 && M1 <= 0) {
                                             ppOnly("TB Handhold- Enrolment", 0, KK, '');
                                             ppneed("TB Handhold- Enrolment + Learning", 0, 0, '');
                                             ppneedBlank("TB Need- Enrolment+Learning", 0, 0, '');
                                         }
                                         else {
                                             alert('TB Need- Enrolment+Learning is already done for TB Handhold- Enrolment!!');
                                             $(th).val('0');
                                             return false;
                                         }
                                     }
                                     if (TxtName == "TB Need- Learning") {

                                         if (KK <= 2 && M1 <= 0) {
                                             ppOnly1("TB Handhold- Learning", 0, KK, '');
                                             ppneed("TB Handhold- Enrolment + Learning", 0, 0, '');
                                             ppneedBlank("TB Need- Enrolment+Learning", 0, 0, '');

                                         }
                                         else {
                                             alert('TB Need- Enrolment+Learning is already done for TB Need- Learning!!');
                                             $(th).val('0');
                                             return false;
                                         }
                                     }
                                     if (TxtName == "TB Need- Enrolment+Learning") {

                                         var HK = ppneedjjValueComine("TB Need- Enrolment", 0, 0, '');

                                         var HK3 = ppneedjjValueComine("TB Need- Learning", 0, 0, '');

                                         var Total = parseFloat(HK3) + parseFloat(HK);



                                         if (Total > 0) {
                                             $(th).val('0');
                                             alert('TB Need entry is already done for Enrollment and Learning!!');
                                             return false;


                                         }
                                         else {
                                             if (KK <= 2) {
                                                 ppOnly2("TB Handhold- Enrolment + Learning", 0, KK, '');
                                             }
                                         }
                                     }
                                 }
                                 else {
                                     if (TxtName == "TB Need- Enrolment") {

                                         ppneed("TB Handhold- Enrolment", 0, 0, '');
                                     }
                                     if (TxtName == "TB Need- Learning") {

                                         ppneed("TB Handhold- Learning", 0, 0, '');
                                     }
                                     if (TxtName == "TB Need- Enrolment+Learning") {

                                         ppneed("TB Handhold- Enrolment + Learning", 0, 0, '');
                                     }
                                 }
                             }
                         }

                         else if (MaxVal == 4) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 4) {
                                     $(th).val('0');
                                     alert('Only 1 can be entered in any 4 months!!.');
                                     return false;
                                 }
                             }
                         }

                         else if (MaxVal == 3) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 1) {
                                     $(th).val('0');
                                     alert('Entry allowed in any one month!!.');
                                     return false;
                                 }
                             }
                         }
                         else if (MaxVal == 10) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 9) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Max 9 MM Can be Planned!! ');
                                 return false;

                             }
                           
                         }
                         else if (MaxVal == 13) {

                             var uh12 = v1;
                             var KK = 0;
                             if (!isNaN(parseFloat(v1))) {
                                 KK = parseFloat($(this).find("input[type='text']").val());
                             }
                             var kkdd = ppneedjjValue("TB Need- Enrolment", 0, 0, '');
                             var Mk = kkdd * 2;
                           
                       
                             if (KK > Mk) {
                                 $(th).val('0');
                                 alert('Only ' + Mk + ' can be entered!!');
                                 return false;
                             }

                         }
                         else if (MaxVal == 14) {

                             var uh12 = v1;
                             var KK = 0;
                             if (!isNaN(parseFloat(v1))) {
                                 KK = parseFloat($(this).find("input[type='text']").val())
                             }
                             var kkdd = ppneedjjLearningValue("TB Need- Learning", 0, 0, '');
                             var Mk = kkdd * 2;
                        
                             if (KK > Mk) {
                                 $(th).val('0');
                                 alert('Only ' + Mk + ' can be entered!!');
                                 return false;
                             }

                         }
                         else if (MaxVal == 14) {

                             var uh12 = v1;
                             var KK = 0;
                             if (!isNaN(parseFloat(v1))) {
                                 KK = parseFloat($(this).find("input[type='text']").val())
                             }
                             var kkdd = ppneedjjLearningValue("TB Need- Enrolment+Learning", 0, 0, '');
                             var Mk = kkdd * 2;

                             if (KK > Mk) {
                                 $(th).val('0');
                                 alert('Only ' + Mk + ' can be entered!!');
                                 return false;
                             }

                         } else if (MaxVal == 15) {

                             var uh12 = parseFloat($(this).find("input[type='text']").val());

                             var kkdd = ppneedjjLearningValue("TB Need- Enrolment+Learning", 0, 0, '');
                             var Mk = kkdd * 2;
                             var KK = 0;
                             if (!isNaN(parseFloat(v1))) {
                                 KK = parseFloat($(this).find("input[type='text']").val())
                             }
                             if (KK > Mk) {
                                 $(th).val('0');
                                 alert('Only ' + Mk + ' can be entered!!');
                                 return false;
                             }

                         }



                     }
                     else if (Type == "3") {
                         if (MaxVal == 11) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                         }
                         else if (MaxVal == 3) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 1) {
                                     $(th).val('0');
                                     alert('Entry allowed in any one month!!.');
                                     return false;
                                 }
                             }
                         }
                         else if (MaxVal == 5) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 6) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Max 6  Can be Planned!!');
                                 return false;

                             }

                             var kkdd = LSGTotal("LSE Sessions", 0, 0, '');
                         
                             if (kkdd > 15) {
                                 $(th).val('0');
                                 alert('Max 15 sessions can be entered!!');
                                 return false;

                             }
                          
                             //if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                             //    uh = uh + 1;

                             //    if (uh > 5) {
                             //        $(th).val('0');
                             //        alert('Only 1 can be entered in any 5 months!!.');
                             //        return false;
                             //    }
                             //}
                         }
                         else if (MaxVal == 1) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 1) {
                                     $(th).val('0');
                                     alert('Entry allowed in any one month!!.');
                                     return false;
                                 }
                             }
                             pp("LSE Sessions", i, parseFloat($(this).find("input[type='text']").val()), v1);
                         }
                         else if (MaxVal == 2) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 2) {
                                     $(th).val('0');
                                     alert('Entry allowed in any two month!!.');
                                     return false;
                                 }
                             }



                         }
                         else if (MaxVal == 6) {

                             if (parseFloat($(this).find("input[type='text']").val()) > 1) {
                                 uh = uh + 1;


                                 $(th).val('0');
                                 alert('Only 1 can be entered!! ');
                                 return false;

                             }
                             if (parseFloat($(this).find("input[type='text']").val()) >= 1) {
                                 uh = uh + 1;

                                 if (uh > 1) {
                                     $(th).val('0');
                                     alert('Entry allowed in any one month!!.');
                                     return false;
                                 }
                             }

                             pp("Learning Endline for GKP", 12, parseFloat($(this).find("input[type='text']").val()), v1);

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

                     if (txt == "TB Need- Enrolment") {

                         if (i >0) {
                             if (idx >= stidx && !isNaN(parseFloat($(this).find("input[type='text']").val())))
                             if (RVal <=0)
                             {
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

     function ppneedBlank(txt, stidx, val, SumValue) {
         var idx = 0;
         var RVal = 0;
         $("[id*=GV_AnnualPlan] tr").each(function (r) {
             if ($(this).find('span').html() == txt) {
                 $(this).eq(0).find('td').each(function (i) {

                     if (txt == "TB Need- Enrolment+Learning") {

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
                <div class="row">
                    <div class="col-lg-2 col-md-2 col-sm-3 clsmain" style="padding-right: 0px;">
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
                    <div class="col-lg-10 col-md-10 col-sm-9">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <div class="row">
                                            <div class="col-lg-6 col-md-6 col-sm-6">
                                                <h3 class="text-danger" style="margin: 0px;">
                                                    Annual Plan</h3>
                                            </div>
                                            <div class="col-lg-6 col-md-6 col-sm-6 " style="padding: 0px">
                                                <input type="image" id="ton-new" class="butt" src="Images/search-not-29.png" title="Search" />
                                                <asp:ImageButton ID="btnDelete" OnClick="btnDelete_Click" CssClass="btn btn-info pull-right"
                                                    ToolTip="Delete" BackColor="#f5f5f5" ImageUrl="~/images/delete-29.png" Style="margin-right: 5px;
                                                    padding: 0px;" runat="server" />
                                                <asp:ImageButton ID="btnsave" CssClass="btn btn-info pull-right" BackColor="#f5f5f5"
                                                    ToolTip="Save" ImageUrl="~/images/save-29-1.png" OnClick="btnsave_Click" 
                                                    Style="margin-right: 5px; padding: 0px;" runat="server" />
                                               
                                            </div>
                                        </div>
                                    </div>
                                    <div>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="row">
                                            <div id="div-show-new">
                                                <div class="row marg search-bg">
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
                                                                        <asp:ListItem Text="Village Level" Value="2"></asp:ListItem>
                                                                        <asp:ListItem Text="School Level" Value="3"></asp:ListItem>
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
                                                                        Panchayat:</label>
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
                                                        <div class="col-lg-1 col-md-1 col-sm-2 cpl-xs-12 col-lg-offset-1 col-md-offset-1 col-sm-offset-1 col-xs-offset-0">
                                                            <asp:ImageButton ID="btnSerach" ToolTip="Serach" runat="server" class="btn btn-danger btn-paddd pull-right"
                                                                BackColor="#f1f1f1" OnClick="btnSerach_Click" ImageUrl="~/images/search-29.png" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
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
                                                                            <asp:Label ID="LblDesc" CssClass="D" Text='<%#Bind("Description") %>' runat="server"></asp:Label>
                                                                            <asp:Label ID="LblLookUp" Text='<%#Bind("LookupCode") %>' Visible="false" runat="server"></asp:Label>
                                                                            <asp:Label ID="lblStartMonth" CssClass="S"   style="color: blue;display:none;" Text='<%#Bind("StartMonth") %>'  runat="server"></asp:Label>
                                                                              <asp:Label ID="lblEndMonth" CssClass="E"   style="color: blue;display:none;" Text='<%#Bind("EndMonth") %>' runat="server"></asp:Label>
                                                                        <asp:Label ID="lblMaxVal" CssClass="M"   style="color: blue;display:none;" Text='<%#Bind("MaxVal") %>' runat="server"></asp:Label>
                                                                  <asp:Label ID="lblLookupType"   style="color: blue;display:none;" Text='<%#Bind("LookupType") %>' runat="server"></asp:Label>
                                               <asp:Label ID="lblPhageFlag"   style="color: blue;display:none;" Text='<%#Bind("PhageFlag") %>' runat="server"></asp:Label>
                                                               
                                                                      
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="20%" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Apr">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtApr" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Apr") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="May">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMay" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("May") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jun">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJun" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Jun") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jul">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJul" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Jul") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Aug">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtAug" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);"  Text='<%#Bind("Aug") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Sep">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtSep" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Sep") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Oct">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtOct" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Oct") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Nov">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtNov" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Nov") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Dec">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtDec" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Dec") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Jan">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtJan" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Jan") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Feb">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtFeb" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Feb") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Mar">
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="TxtMar" MaxLength="3" Enabled="false" CssClass="form-control cMay"
                                                                                onchange="return onEvent(this);" Text='<%#Bind("Mar") %>' runat="server" onkeypress="return isNumberKey(this,event);"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </div>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

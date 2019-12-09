//jQuery(document).ready(function() {
	
    if (!window.console) {
        window.console = function() {};
    }
	
    var self = this;
	
    var errorMessage;

    self.validate = function () {

       //$(".inputs").keyup(function () {
       //    if (this.value.length == this.maxLength) {
       //        var $next = $(this).next('.inputs');
       //        if ($next.length)
       //            $(this).next('.inputs').focus();
       //        else
       //            $(this).blur();
       //    }
       //});
        
        //alert('You\'ve been validated');
    }



 
    // $('#DateOfBirth').datetimepicker();

    
    //$(function () {
    //    $(".jqueryui-marker-datepicker").datepicker({
    //        dateformat: "dd-mm-yy",
    //        changeYear: true,
    //        showOn: "button"
    //    }).css("dsiplay", "inline-block").next("button")({
    //        icons: { primary: "ui-icon-calendar" },
    //        label: "Select a date",
    //        text: false
    //    });
    //});

    //Function to get form list conditional upon what is selected in the study list combo box
//TODO: the form list drop down will be hidden initially then rendered on change

    //self.getDatePicker = function()
    //{
    //    $('#DateOfBirth').datepicker();
    //}

    self.getFormList = function()
    {
        var studyId;
        studyId = $('#ddlStudyName').val();

        $.ajax({
                type: "post",
                url: "/StudyPersonSample/GetFormList",
                data: { studyId: $('#ddlStudyName').val() },
                datatype: "json",
                traditional: true,
                success: function (data) {
                    var formName = "<select id='ddlDEFormName'>";
                    formName = formName + '<option value="">--Select--</option>';
                    for (var i = 0; i < data.length; i++) {
                        formName = formName + '<option value=' + data[i].Value + '>' + data[i].Text + '</option>';
                    }
                    formName = formName + '</select>';
                    $('#ddlDEFormName').html(formName);
                    $('#txtStudyID').val(studyId);
                    
                    var value
                    value = $('#txtStudyID').val();
                    //alert (value);
                }
        });
    }

    self.getGender = function () {
        var participantType
        var sex
        sex = "Female";        
        participantType = $('#ddlParticipantType').val();

        if (participantType == 'Mother') {
            $('#Sex > option').each(function () {
                if ($(this).text() == sex) $(this).parent('select').val($(this).val())
            });
            sex = $('#Sex').val();
            //alert("The gender of the participant has been changed to  = " + sex);
        } else {
            //Do nothing
        }
    }

    self.getStudyForm = function()
    {
        var studyId
        var studyName
        var viewName  // N.B. viewName is both a viewName and also and name of an ActionResult method
           //var formId
             
        studyId = $('#txtStudyID').val();
        //formId = $('#ddlDEFormName').val();
        studyName = $('#ddlStudyName').children('option:selected').text();  //$("#box1 option:selected").text();  $('#yourdropdownid').children("option:selected").text();
        pscId = $('#txtPSCID').val();
        viewName = $('#ddlDEFormName').val(); 
        urlString = '/StudyPersonSample/' + viewName + '?PSCID=' + pscId + '&StudyID=' + studyId +'&StudyName=' +studyName;
        urlString2 = '/StudyPersonSample/AddForm?PSCID=' + pscId + '&StudyID=' + studyId + '&ViewName=' + viewName + '&StudyName=' +studyName;

        // alert('The study ID is ' + studyId + ' and the form ID is ' + viewName + ' AND THE URL is ' + urlString);
        //alert(studyName);

        if (studyId != '' && pscId != '' && viewName != '') {

            window.location.href = urlString; // // urlString2

        }
        else {
            if (studyId == '') {
                alert('Please select a study. Thank you');
            }
            if (pscId == '') {
                alert('Please scan in a Person Collection Sample ID. Thank you.');
            }
        }
    }

    self.getStudyFormByID = function () {
        var studyId
        var studyName
        var formBarcode
        var pscId
        var SampleProcessing
        var formURLString
        var gURL = '/StudyPersonSample/GetStudyFormByID'  //N.B. /BioSys prefix should be removed when running locally
        //studyId = $('#txtStudyID').val();
        studyId = $('#ddlStudyName').val();
        studyName = $('#ddlStudyName').children('option:selected').text();
        pscId = $('#txtPSCID').val();
        formBarcode = $('#txtFormIDBarcode').val();
        SampleProcessing = '0'  // by default, sample processing div sections are hidden until collection and processing status is validated
        if (studyId != '' && formBarcode != '' && pscId != '') {
            $.ajax({
                type: 'POST',
                url: gURL, //'/StudyPersonSample/GetStudyFormByID',
                //data: { formBarcode },
                data: { formBarcode, pscId, studyId},
                datatype: 'json',
                traditional: true,
                success: function (data) {
                    //formURLString = '/StudyPersonSample/' + data[0].ViewName + '?PSCID=' + pscId + '&StudyID=' + studyId + '&StudyName=' + studyName;
                    //window.location.href = formURLString;
                    //if (data[0].ViewName != '') {
                        formURLString = '/StudyPersonSample/AddForm?PSCID=' + pscId + '&StudyID=' + studyId + '&ViewName=' + data[0].ViewName + '&StudyName=' + studyName + '&SampleProcessing=' + SampleProcessing;
                        $('#SampleProcessing').children().prop('disabled', true);
                        window.location.href = formURLString;
                    //} else {

                        //formURLString = '/StudyPersonSample/AddStudyPerson';
                        //$('#PSCID').Val(pscId);
                        //window.location.href = formURLString;

                    //}                   

                }
            });
        } else {
            if (studyId == '') {
                alert('Please select a study. Thank you');
            }
            if (pscId == '') {
                alert('Please scan in a Person Collection Sample ID. Thank you.');
            }
        }
    }

    self.collectSample = function (SampleType) {
        var studyId
        var pscId
        //var sampleType
        var sampleSetName
        var dateOfCollection
        var timeOfCollection
        var collectedBy
        var fasting

        var buccalCollected
        var cordBloodCollected
        var eDTACollected
        var gelCollected
        var litHepCollected
        var oxalateCollected
        var salivaCollected
        var serumCollected
        var urineCollected

        var birthType
        var multipleBirthOrder
        var storageLocationName
        var boxNumber
        var position
        var reasonNotCollected
        var reasonNotcollectedOther

        //var buccal
        //var eDTA
        //var gel
        //var litHep
        //var oxalate
        //var serum
        //var urine

        //set variable flags
        buccalCollected = $('#BuccalCollected').val();
        eDTACollected = $('#EDTACollected').val();
        gelCollected = $('#GelCollected').val();
        litHepCollected = $('#LitHepCollected').val();
        oxalatCollectede = $('#OxalateCollected').val();
        serumCollected = $('#BuccalCollected').val();
        urineCollected = $('#UrineCollected').val();


        if (buccalCollected = "Yes") {
            buccal = "1";
        } else {
            buccal = '';
        }        
        if (eDTACollected == "Yes") {
            eDTA = "1";
        } else
        {
            eDTA = '';
        }
        if (gelCollected == "Yes") {
            gel = "1";
        } else {
            gel = '';
        }
        if (litHepCollected == "Yes") {
            litHep = "1";
        } else {
            litHep = '';
        }
        if (oxalatCollectede == "Yes") {
            oxalate = "1";
        } else {
            oxalate = '';
        }
        if (serumCollected == "Yes") {
            serum = "1";
        } else {
            Serum = '';
        }
        if (urineCollected == "Yes") {
            urine = "1";
        } else {
            urine = '';
        }
        
        
        studyId = $('#StudyID').val();
        pscId = $('#PSCID').val();
        dateCollected = $('#DateOfCollection').val();
        timeCollected = $('#TimeOfCollection').val();
        
        //TODO: SEE IF VALUES GET SET TO '' IF FIELDS DON'T EXIST I.E. ADD TO STRING AND SEE IF IT FAILS IF NOTHING EQUIVALENT IS PASSED FROM THE FORM!!!
        alert('Collect Sample function triggered for ' + pscId + 'for study #' + studyId + ' where sample type is ' + SampleType + ' and was collected on ' + dateCollected + ' at ' + timeCollected)
        if (pscId != '' && studyId != '' && dateCollect != '' && timeCollected != '') {

            //TODO: build xml
            //TODO: pass it to the controller
            //$.ajax({
            //    type: 'POST',
            //    url: '/StudyPersonSample/CollectSample',
            //    data: { pscId, studyId, SampleType, dateCollected, timeCollected },
            //    datatype: 'json',
            //    traditional: true,
            //    success: function (data) {

            //        //do something

            //    }

            //});
        }


    }
    self.returnToIndex = function(){

    }

    self.getParticipantDetails = function ()
    {
        var pscId
        var studyId
       

         studyId = $('#txtStudyID').val();
         pscId = $('#txtPSCID').val();

         //console.log('you reached Level A');

         if (studyId != '' && pscId != '') {
            $.ajax({
                type: 'GET',
                url: '/StudyPersonSample/GetPersonByPSCID',                 // REM: ADD /BioSys prefix
                //                data: { pscId: $('#txtPSCID').val() },
                data: { pscId, studyId },
                datatype: "json",
                traditional: true,
                success: function (data) {

                    //alert('you got to success');
                    //console.log(JSON.stringify(data));

                    loadPersonSnippet(data);
                }
            });            
        }
        else {
            if (studyId == '')
            {
                alert('Please select a study. Thank you');
            }
            if (pscId == '')
            {
                alert('Please scan in a Person Collection Sample ID. Thank you.');
            }
        }
    }

    function loadPersonSnippet(data) {
        var studyPersonExists;
        var tab = $('<table></table>');
        var thead = $('<thead></thead>');
        //thead.append('<th>PSCID</th>');
        thead.append('<th>Person Details</th>');

        tab.append(thead);
        $.each(data, function (i, val) {
            var trow = $('<tr></tr>');
            //trow.append('<td>' + val.PSCID + '</td>');
            trow.append('<td>' + val.PersonResult + '</td>');
            if (val.PersonExists != '1')
            {
                //COMMENTED OUT - DEMOGRAPHICS WILL NOW BE ADDED BY STUDY SPECIFIC FORM
                //trow.append('<td> <a class="btn btn-success" href="/StudyPersonSample/AddStudyPerson?PSCID=' + val.PSCID + '&StudyID=' + val.StudyID + '")">Add Participant</a> </td>')
            }
            tab.append(trow);

            studyPersonExists = val.PersonExists;
            
        });
        //$('#personSnippetPanel').html(tab);   
        $('#personSnippetResult').html(tab);
        console.log('Does person exist? ' + studyPersonExists);
        //TODO: ??ADD DOES PERSON EXIST TO DUMMY FIELD??
    };

    function addStudyPerson() {

        var pscId
        var studyId
        var studyName
        var participantType
        var dob
        var sex
        var studyPersonId

        pscId = $('#PSCID').val();
        studyId = $('#ddlStudyName').val();
        studyName = $('#ddlStudyName option:selected').text();
        participantType = $('#ddlParticipantType').val();
        dateOfBirth = $('#DateOfBirth').val();
        sex = $('#Sex option:selected').text();
        studyPersonId = $('#StudyPersonID').val();
        var gURL = '/StudyPersonSample/AddStudyPerson';     // REM: ADD /BioSys prefix

        // alert('Add Study Person ' + pscId + ' for Study ' + studyId + ' who is a ' + ParticipantType + ' born ' + DateOfBirth + ' and is ' + Sex + ' with the Study Person ID ' + StudyPersonId + '.');

        if (studyId != '' && pscId != '') {
            $.ajax({
                type: 'POST',
                url: gURL,   //  LOCALHOST MODE: '/StudyPersonSample/AddStudyPerson',
                data: { pscId, studyName, studyId, participantType, dateOfBirth, sex, studyPersonId },
                datatype: "html", // //'json',
                traditional: true,
                success: function (data) {
                    //alert('Added Study Person ' + pscId + ' for Study ' + studyId + '.');
                    //var urlRedirect
                    //urlRedirect = '/Home/Index';
                    //alert(data.redirecturl);
                    window.location.href = data.redirecturl;
                }
                //async: false
            });

        }
        else {
            if (studyId == '') {
                alert('Please select a study. Thank you');
            }
            if (pscId == '') {
                alert('Please scan in a Person Collection Sample ID (PSCID). Thank you.');
            }
        }
    }

    function getStudy(){
        //alert("A form has been scanned!");
        var FormID = $('#txtFormIDBarcode').val();
        var gURL = '/StudyPersonSample/GetStudyByFormID' // REM: ADD /BioSys prefix     //LOCALHOST MODE:  '/StudyPersonSample/GetStudyByFormID';
        $.ajax({
            type: 'POST',
            url: gURL,         //LOCALHOST MODE:  '/StudyPersonSample/GetStudyByFormID',
            data: { FormID },
            datatype: 'json',
            traditional: true,
            success: function (data) {
                $('#ddlStudyName').val(data);
            }            
        });
    }


    function clearBarcodeField(field) {
            jQuery('#' + field).val('');
            //if (field == 'pregnancyIdCheck') {
            //    jQuery('.sampleProcessing').hide();
        }
 
//});
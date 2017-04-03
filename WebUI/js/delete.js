
	$(function() {
	    $("#cards").keydown(function (event) {
			$('#form_Groupscard #cardidDiv').hide();
	        if (event.keyCode == "13") {
				showcount();
	        }
	    });
	});
	
	function showcount(){
		var counter = 0;
		var str = $("#cards").val();
		var shuzu=str.split("\n");
		for(var i=0; i<shuzu.length; i++){
			if($.trim(shuzu[i])!="")
			counter +=1;
		}
		$("#form_Groupscard #Groupscount").html(counter);
	}
    function batchSubmit() {
    	var facevaluecheck = $('#batchForm input:radio[name="facevalue"]').is(":checked");
		if (!facevaluecheck) {
			alert('请选择面值');
			return false;
		}
		var cardcontent = $("#form_Groupscard #cardcontent").val();
		if(cardcontent==''){
			alert('请输入卡号和密码');
			$('#form_Groupscard #cardcontent').focus();
			return false;
		}
		m2_getCardid(document.getElementById("cardcontent"));
		var cardid = $('#form_Groupscard #cardid').val();
		if($.trim(cardid)==''){
			alert('该卡种不能识别');
			return false;
		}
        $('#form_Groupscard #btn_batch').attr('disabled', "true");
        var sSource = '/recycle/batchsubmitcard';
        var postData = $('#batchForm').serializeArray();
        
        $("#customchar").val('');
		return false;
    }

    function m2_getCardid(obj) {
       
        replacedata(obj, false);
        
    	//showcount();
    	var content=obj.val();
    	content=content.split('\n')[0].split(' ');
    	if(content.length!=2){
    		return;
    	}
		var cardsn = $.trim(content[0]);
		var cardpsw = $.trim(content[1]);
		if(cardsn==''||cardpsw==''){
			return;
		}
		var url="/recycle/getCardid?cardsn="+cardsn+"&cardpsw="+cardpsw+"&_="+new Date().getTime();
		
	}
	function cleanWords() {
	    
    	var cleanWord = $('#form_Groupscard #cleanWord').val();
    	if(cleanWord=='')
    	{
    		return;
  }
    	var cardcontent = $("#cards");
    	cardcontent.val(cardcontent.val().replace(new RegExp(cleanWord,"gm"), ''));
    	m2_getCardid(document.getElementById("cards"));
    }
    function replacedata(obj,cardindx) {
        var result = obj.val();
        result = result.replace(/[^0-9a-zA-Z]/g, ' ');
        result = ' ' + result.replace(/\s/g, '  ') + ' ';
        result = result.replace(/\s\S{1,5}\s/g, ' ');
        result = result.replace(/\s+/g, ' ');
        var temparry = $.trim(result).split(' ');
        if(temparry.length % 2 != 0||temparry.length < 2) {
            
        	return;
        }    
		if(cardindx)
		{
			var temp = temparry[0];
			temparry[0] = temparry[1];
			temparry[1] = temp;
		}

var tempstrcontent = "";
		for (var i = 0; i < temparry.length; i = i + 2) 
		{
			if( temparry[0].length == temparry[1].length)
			{
				tempstrcontent+=temparry[i];
				tempstrcontent+=" ";
				tempstrcontent+=temparry[i+1];
			}
			else if (temparry[i].length == temparry[1].length
					&& temparry[i+1].length == temparry[0].length) 
			{
				tempstrcontent+=temparry[i+1];
				tempstrcontent+=" ";
				tempstrcontent+=temparry[i];
			}
			else
			{
				tempstrcontent+=temparry[i];
				tempstrcontent+=" ";
				tempstrcontent+=temparry[i+1];
			}
			tempstrcontent+="\n";
		}
		if(tempstrcontent != "")
		{
		    obj.val(tempstrcontent);
		    
}
    }

	function cleanup() {
	    var cardcontent = $("#cards").val();
		if (cardcontent == '') {
			alert('请输入卡号和密码');
			$('#cards').focus();
			return false;
		}
		var sSource = '/recycle/cleanupcardcontent';
		var postData = 'cardcontent=' + encodeURIComponent(cardcontent);
		
		return false;
	}

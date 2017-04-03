﻿// JavaScript Document
/**
 * create the prototype of trim() on the String object	
 * Engineer:wenliming 
 */
String.prototype.trim = function() 
{	
 	// skip leading and tailing whitespace and return everything in between
  	return this.replace(/^\s*(\b.*\b|)\s*$/, "$1");	
}

String.prototype.leftTrim = function()
{
	return this.replace(/^\s*(\b.*|)\s*$/, "$1");
}
String.prototype.rightTrim = function()
{
	return this.replace(/^\s*(.*\b|)\s*$/, "$1");
}

String.prototype.equalsIgnoreCase = function(target)
{
	if (target == null) return false;
	return target.toLowerCase() == this.toLowerCase();
} 

function removeSpChar(obj){
	obj.value = obj.value.trim();
	obj.value = obj.value.replace(/[\'\"\:\;\<\>\,\?\/\`\~\!#$%\^\&\*\|\\\+\-\= ]+/g,'');
}
function removeSpChar_Self(obj){
	obj.value = obj.value.trim();
	obj.value = obj.value.replace(/[\' ]+/g,'');
}
/**
 * Remove illegal chars from values of  
 * INPUT(type is not hidden) and TEXTAREA of form.
 * @param string
 */
function removeIllegalCharFromForm(form)
{
	if (form == null || typeof(form)!="object") return;
	var it;
	for (var i=0; i<form.length; i++)
	{
		it = form.item(i);
		if(it.tagName!="INPUT" && it.tagName!="TEXTAREA") continue;
		if (it.type == "hidden") continue;
		it.value = removeIllegalChar(it.value);
	}
}

/**
 * Remove illegal chars from string.
 * @param str String
 */
function removeIllegalChar(str)
{
	var result = str;
	if (result == null) return null;
	result = result.trim();
	if (result != "") 
	{
		// result = result.replace(/'/gi,"");
		// result = result.replace(/'|"|!|%|\\|\/|<|>/gi,"");
		// under repalcing is for web page.
		// result=result.replace(/&/gi,"\\&amp;");               
		// result=result.replace(/</gi,"\\&lt;");
		// result=result.replace(/>/gi,"\\&gt;");
	}
	return result;
}

/**
 * Check whether input is numeric greater 0. if not, focus the field, and make the filed selected
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isStringNumGreatZeroObj(obj)
{
	var result = false;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;
	
	
	result = isStringNum(value);
	if (!result || parseInt(value) <= 0)
	{
		alert(getMessage("MSG_SYS_030"));
		obj.focus();
		obj.select();
		result = false;
	}
	return result;
}

/**
 * Check whether input is Double numeric. if not, focus the field, and make the filed selected
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isStringDoubleNumObj(obj)
{
	var result = false;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;
	
	
	result = isStringNum(value);
	if (!result || parseInt(value) == 0 || parseInt(value)%2 == 1)
	{
		alert(getMessage("MSG_S0_0053"));		
		obj.focus();
		obj.select();
	}
	return result;
}
/**
 * Check whether input is numeric. if not, focus the field, and make the filed selected
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 * add param : zeroflag  boolean ,  true:value == 0 OK   false: value ==0 fail
 */
function isStringNumObj(obj,intmax,dotmax,neg,zeroflag)
{
	var result = false;
	if (obj == null) return false;

	var value = obj.value.trim();
	obj.value = value;
	if (value == "") return true;
	
	if (value == ".")
	{
		if(dotmax > 0)
			alert(getMessage("MSG_SYS_064",intmax,dotmax));
		else
			alert(getMessage("MSG_SYS_027",intmax));
		obj.value = "";
		obj.focus();
		obj.select();
		return false;
	}
	if (zeroflag != null && !zeroflag)
	{
		var v1 = parseFloat(value);
		if (v1 == 0)
		{
			alert(getMessage("MSG_SYS_030"));
			obj.value = "";
			obj.focus();
			obj.select();
			return false;
		}
	}

	result = isStringNumOrg(value,intmax,dotmax,neg);

	if (!result)
	{
		if(dotmax > 0)
			alert(getMessage("MSG_SYS_064",intmax,dotmax));
		else
			alert(getMessage("MSG_SYS_027",intmax));
		obj.focus();
		obj.select();
		return false;
	}
	
	appendDotZero(obj,dotmax);
	
	return result;
}

/*
 * validate a Digital string. include negative digital.
 * @param String str to validate
 * @return boolean
 */
function isStringNumOrg(str,intmax,dotmax,neg)
{
	var result = true;
   	if(str == null || str.trim() == "") return false;
	
	str = str.trim();
	
	var dotnum = 0;
	var numlen = str.length;
	var dotlen = 0;
	for(var i=0; i<str.length; i++)
    {
        var strSub = str.substring(i,i+1);         
        if( !(strSub<="9"&&strSub>="0") )
        {
        	if(strSub==".")
        	{
        		dotnum = dotnum + 1;
        		numlen = numlen -1;
        		dotlen = str.length - i -1;
        		if(dotnum > 1)
        		{
					result = false;
					break;
				}	
        		continue;
        	}
        	if(neg)
        	{
        		if(!(strSub=="-" && i==0))
				{
					result = false;
					break;
				}
				else
					numlen = numlen -1;
        	}
        	else
        	{
				result = false;
				break;
			}
        }
    }
    numlen = numlen - dotlen;
    if(result)
    {
	    if(dotmax > 0)
	    {	    	
	    	if(result && (intmax > 0) && (intmax < numlen )) 
	    		result = false;
	    	if(result && (dotmax < dotlen )) 
	    		result = false;
	    }
	    else
	    {
	    	if(result && (dotlen > 0)) 
	    		result = false;
	    	if(result && (intmax > 0) && (intmax < numlen )) 
	    		result = false;
	    }
	}
    return result ;	
}

//小数补0
function appendDotZero(obj,dotmax)
{
	if (dotmax > 0)
	{
		value = obj.value;
		value = value.trim()
		if(""!=value){
			
			if(value.indexOf(".") == -1)
			{
				//没有小数
				value += "."
					for(var i=0; i<dotmax; i++)
						value += "0";
				obj.value = value;
			}
			else
			{
				//有小数
				var index = value.indexOf(".")+1;
				var tmpStr = value.substring(index,value.length);
				for(var i=0; i< dotmax; i++)
				{
					if(tmpStr.charAt(i) == "")
						tmpStr += "0";
				}
				obj.value = value.substring(0,index) + tmpStr;
			}
		}
	}
}
/*
 * validate a Digital string.
 * @param String str to validate
 * @return boolean
 */
function isStringNum(str)
{
	if (str == null || str == "") return false;
	reg = /^\d{1,50}?$/gi;
	return reg.test(str);
}

/*
 * validate a Digital string. include negative digital.
 * @param String str to validate
 * @return boolean
 */
function isStringNumEx(str)
{
	var result = true;
   	if(str == null || str.trim() == "") return false;
	
	str = str.trim();
	if(str.length == 1 && str == "-")
	{
		return false;
	}

    for(var i=0; i<str.length; i++)
    {
        var strSub = str.substring(i,i+1);         
        if( !((strSub<="9"&&strSub>="0") || (strSub==".")) )
        {
			result = false;
			break;
        }
    }
    return result ;	
	
}


/*
 * validate a Digital or Letter string
 * @param String str to validate
 * @return boolean
 */
function isDigitalOrLetter(str)
{
	var result = true;
   	if(str == null || str.trim() == "") return false;
	
	str = str.trim();

    for(var i=0; i<str.length; i++)
    {
        var strSub = str.substring(i,i+1);         
        if( !((strSub<="9"&&strSub>="0") || 
		    (strSub<="Z"&&strSub>="A") || 
			(strSub<="z"&&strSub>="a")) )
        {
			result = false;
			break;
        }
    }
    return result ;	
}

/**
 * Check whether input is digitals delim by comma string. 
 * if not, focus the field, and make the filed selected.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isDigitalDelimCommaObj(obj)
{
	var result = false;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;

	result = isDigitalDelimComma(value);
	if (!result)
	{
		alert(getMessage("MSG_S0_0048"));
		obj.focus();
		obj.select();
		return result;
	}
	removeErrorComma(obj);
	return result;
}

/*
 * validate a Digital or Comma string
 * @param String str to validate
 * @return boolean
 */
function isDigitalDelimComma(str)
{
	var result = true;
   	if(str == null || str.trim() == "") return false;
	
	str = str.trim();

    for(var i=0; i<str.length; i++)
    {
        var strSub = str.substring(i,i+1);         
        if( !((strSub<="9"&&strSub>="0") || (strSub==",")) )
        {
			result = false;
			break;
        }
    }
    return result ;	
}
/**
 * Check whether input is digital or letter delim by comma string. 
 * if not, focus the field, and make the filed selected.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isDigitalLetterDelimCommaObj(obj)
{
	var result = false;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;

	result = isDigitalLetterDelimComma(value);
	if (!result)
	{
		alert(getMessage("MSG_S0_0049"));
		obj.focus();
		obj.select();
		return result;
	}
	removeErrorComma(obj);
	return result;
}

/*
 * validate a Digital or Letter or Comma string
 * @param String str to validate
 * @return boolean
 */
function isDigitalLetterDelimComma(str)
{
	var result = true;
   	if(str == null || str.trim() == "") return false;
	
	str = str.trim();

    for(var i=0; i<str.length; i++)
    {
        var strSub = str.substring(i,i+1);         
        if( !((strSub<="9"&&strSub>="0") || 
		      (strSub<="Z"&&strSub>="A") || 
			  (strSub<="z"&&strSub>="a") || (strSub==",")) )
        {
			result = false;
			break;
        }
    }
    return result ;	
}
/*
 *delete error comma in the obj.value
 * @param  obj  
 *exampe:
 *translate 123,,456,,,789 to 123,456
 *translate ,123,456, to 123,456
*/
function removeErrorComma(obj)
{
	var value = obj.value.trim();
	while ((value.length>0) && (value.indexOf(",,")>=0))
	{
		var p = value.indexOf(",,");
		value= value.substring(0,p) + value.substring(p+1,value.length);
	}
	if (value.length>0 && value.substring(0,1)==",")
	{
		value= value.substring(1,value.length);
	}
	if (value.length>0 && value.substring(value.length-1,value.length)==",")
	{
		value= value.substring(0,value.length-1);
	}
	obj.value = value;
}


/*
 * validate a date
 * @param String sDate string date to validate
 * @param String sSpit split
 * @return boolean
 */
function isDateString(sDate, sSpit)
{	var iaMonthDays = [31,28,31,30,31,30,31,31,30,31,30,31];
	var iaDate = new Array(3);
	var year, month, day;

	iaDate = sDate.split(sSpit);
	if (iaDate.length != 3) return false;
	if (iaDate[1].length > 2 || iaDate[2].length > 2) return false;
	
	//check numeric
	if(!isStringNum(iaDate[0]) || !isStringNum(iaDate[1]) || !isStringNum(iaDate[2]) )
	{
		return false;
	}

	year = parseFloat(iaDate[0]);
	month = parseFloat(iaDate[1]);
	day = parseFloat(iaDate[2]);

	if (year < 1900 || year > 2200) return false;
	if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) iaMonthDays[1]=29;
	if (month < 1 || month > 12) return false;
	if (day < 1 || day > iaMonthDays[month - 1]) return false;
	return true;
}

/**
 * format date string to YYYY/MM/DD
 * @param  dateString  
 * @param  spit
 * @return boolean  
 * @note   call this function in onblur
 */
function formatDate(dateString, spit)
{
	var result = null;
	if(!isStringNum(dateString)) return dateString;
	
	var today = new Date();
	var year = today.getYear()+"";
	var month = today.getMonth()+1+"";
	var day = today.getDate()+"";
	month = (month.length == 1) ? ("0" + month) : month;
	day = (day.length == 1) ? ("0" + day) : day;
	
	switch (dateString.length)
	{
		case 1:  // d or today
			if(dateString == "0")	// 0 for today
				result = year + spit + month + spit + day;
			else
				result = year + spit + month + spit + "0" + dateString;
			break;
		case 2:  // dd
			result = year + spit + month + spit + dateString;
			break;
		case 3:  // mdd
			result = year + spit + "0" + dateString.substring(0, 1) + spit + dateString.substring(1, 3);
			break;
		case 4:  // mmdd
			result = year + spit + dateString.substring(0, 2) + spit + dateString.substring(2, 4);
			break;
		case 5:  // ymmdd
			result = year.substring(0,3) + dateString.substring(0, 1) + spit + 
					dateString.substring(1, 3) + spit + dateString.substring(3, 5);
			break;
		case 6:  // yymmdd
			year = dateString.substring(0, 2);
			year = (parseFloat(year) > 70)?("19" + year):("20" + year);
			result = year + spit + dateString.substring(2, 4) + spit +
					 dateString.substring(4, 6);
			break;
		case 7:  // yyymmdd
			year = dateString.substring(0, 3);
			year = (parseFloat(year) < 100)?("2" + year):("1" + year);
			result = year + spit + dateString.substring(3, 5) + spit +
					 dateString.substring(5, 7);
			break;
		case 8:  // yyyymmdd
			result = dateString.substring(0, 4) + spit + dateString.substring(4, 6) + spit +
					 dateString.substring(6, 8);
			break;
	}
	return result;
}

/*
 * validate a time string it must be like 15:30
 * @param String sTime string time to validate
 * @param String sSpit split
  * 
 * @return boolean
 */
function isTimeString(sTime, sSpit)
{
	var iaTime = new Array(2);
	var hour, min;

	iaTime = sTime.split(sSpit)
	if (iaTime.length != 2) return false
	if (iaTime[0].length > 2 || iaTime[1].length > 2) return false
	
	//check numeric
	if(!isStringNum(iaTime[0]) || !isStringNum(iaTime[1]) )
	{
		return false;
	}

	hour = parseInt(iaTime[0]);
	min = parseInt(iaTime[1]);

	if (hour < 0 || hour > 23) return false;
	if(min < 0 || min > 59) return false;		

	return true
}

/**
 * format time string to HH:MI
 * @param  time  
 * @param  spit
 * @return boolean  
 * @note   call this function in onblur
 */
function formatTime(timeString, spit)
{
	var result = null;
	if(!isStringNum(timeString)) return timeString;
	
	var today = new Date();
	var hour = today.getHours();
	var minute = today.getMinutes();
	hour = (hour.length == 1) ? ("0" + hour) : hour;
	minute = (minute.length == 1) ? ("0" + minute) : minute;
	switch (timeString.length)
	{
		case 1:  // H or today
			if(timeString == "0")	// 0 for today
				result = hour + spit + minute;
			else
				result = "0" + timeString + spit + "00";
			break;
		case 2:  // HH
			result = timeString + spit + "00";
			break;
		case 3:  // HHM
			result = timeString.substring(0, 2) + spit + timeString.substring(2, 3) + "0";
			break;
		case 4:  // HHMI
			result = timeString.substring(0, 2) + spit + timeString.substring(2, 4);
			break;
	}
	return result;
}

function judgeMoneyObj(obj, isIncNegative, msgID)
{
	if (obj == null) return false;
	var value = obj.value.trim();
	value = value.replace(/,/gi,"");
	if (value == "") return true;
	
	var isLegal;
	if(isIncNegative)
	{
		isLegal = isStringNumEx(value);
	}
	else
	{
		if(value.substring(0, 1) == "-")
		{
			alert(getMessage(msgID));
			obj.focus();
			obj.select();
			return false;
		}
		isLegal = isStringNum(value);
	}
	
	if (!isLegal)
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();				
		return false;
	}
	var isZero = true;
	for (var i=0; i<value.length; i++)
	{
		if (value.substring(i, i+1) != "0")
		{
			value = value.substring(i, value.length);
			isZero = false;
			break;
		}
	}
	if (isZero) value = "0";
	value = formatMoney(value);
	obj.value = value;
	return true;	
}

/**
 * format money string to 000,000,000
 * @param  moneyString  
 * @param  spit
 * @return boolean  
 * @note   call this function in onblur
 */
function formatMoney(moneyString)
{
	var result = "";
	if (moneyString == null) return moneyString;
	
	var count = Math.ceil(moneyString.length/3);
	if (count == 1)
		return moneyString;
	
	var isNegative = false;
	if(parseInt(moneyString) < 0)
	{
		isNegative = true;
		moneyString = moneyString.substring(1, moneyString.length);
	}
	count = Math.ceil(moneyString.length/3);
	
	for (var i=1; i<count;i++)
	{
		result = "," + moneyString.substring(moneyString.length-3*i, moneyString.length-3*(i-1)) + result;
	}
	result = moneyString.substring(0, moneyString.length-3*(i-1)) + result;
	
	if(isNegative)
	{
		result = "-" + result;
	}
	
	return result;
}

/**
 * validate a katakara string .
 * 12449-12531
 * if katakara is invalidate then return false
 * else return true 
 */
  
function isKatakaraObj(obj) 
{
	if (obj == null) return false;
	var value = obj.value.trim();
	if (value == "") return true;
      	
	if(!isKatakaraString(value))	
	{
		alert(getMessage("MSG_S0_0021"));
		obj.focus();
		obj.select();
		return false;
	}
	return true;
}
/* check char is or not a full part digital letter
 * @param char
 */
function isFullDigital(char)
{
	//if (char < '0' || char > '9')
	if (char == '０' || char == '１' || char == '２' || char == '３' || char == '４'
	 || char == '５' || char == '６' || char == '７' || char == '８' || char == '９')
	{
		return true;
	}
	return false;
}
/* check char is or not a full part letter
 * @param char
 */
function isFullLetter(char)
{
	if ((char < 'ａ' || char > 'ｚ') && (char <'Ａ' || char > 'Ｚ') && char != "　")
	{
		return false;
	}
	return true;
}
/* check char is or not a half part letter
 * @param char
 */
function isHalfDigital(char)
{
	if (char < '0' || char > '9')
	{
		return false;
	}
	return true;
}
/* check char is or not a half part letter
 * @param char
 */
function isHalfLetter(char)
{
	if ((char < 'a' || char > 'z') && (char <'A' || char > 'Z'))
	{
		return false;
	}
	return true;
}

/* check char is or not a half part letter includes space,comma,star....
 * @param char
 */
function isHalfLetterEx(strSub)
{
	/*if ((char < 'a' || char > 'z') && (char <'A' || char > 'Z'))
	{
		return false;
	}*/
	if( !(strSub<="~" && strSub>=" ") ||
			  strSub=="'" ||
			  strSub=="%" ||
			  strSub=="?")
	{
		return false;
	}
	return true;
}

/*
 * @param katakara string to validate
 * @return boolean
 */
function isKatakaraString(katakara)
{
	/*for(i=0; i<katakara.length; i++)
	{
		var char = katakara.charAt(i);
		
		if(i == 0 || i== (katakara.length-1))
		{
			if(char == "　")
				return false;
		}
		
		if(!isHalfKataChar(char) && !isHalfKatakaraS(char) && !isFullDigital(char) && !isHalfDigital(char) 
			 && !isFullLetter(char) && !isHalfLetterEx(char))
    	{
			return false;
		}
	}*/
	return true;
}

function isFullKataChar(str)
{
	return (str >= 'ア' && str <= 'ン');
}

function isHalfKataChar(str)
{
	return (str >= 'ｦ' && str <= 'ﾟ');
}

/**
  *@param char little chat to validate
  *@return boolean
  */
function isHalfKatakaraS(str)
{
	var result = false;
	switch(str)
	{
		case 'ァ' :
		case 'ィ' :
		case 'ゥ' :
		case 'ェ' :
		case 'ォ' :
		case 'ヶ' :
		case 'ャ' :
		case 'ュ' :
		case 'ョ' :
		case 'ッ' :
		case 'ー' :
		case '?' :
			result = true;
			break;
		default:
	}
	return result;
}

/**
 * Limit the Text or TextAres length
 * @param obj, check obj, can user this point
 * @param length ,the limit length
 */
function limitLength(obj,length)
{
	if(obj == null) return true;	
	var value = obj.value.trim();
	if(value == "") return true;	
	if(value.length > parseInt(length))
	{
		alert(getMessage("MSG_S0_0022", length));
		obj.focus();
		return false;
	}
	return true;
 }
 
/**
 * Check Empty.
 * @param array the name of can't be empty object.
 * @return boolean
 */
function isAnyMustBeInputEmpty(array)
{
	var result = false;
	for (var i=0; i<array.length; i++)
	{
		obj = document.all(array[i]);
		if (obj == null) continue;
		var value = obj.value.trim();
		if(value == "") 
		{
			alert(getMessage("MSG_S0_0010"));
			if(!obj.disabled)
			{
				obj.focus();
			}
			result = true;
			break;
		}
	}
	return result;
}

/**
 * Check the value is or is not existed in db.
 * @param key 
 * @param val value
 * @param recallFunction
 * @return void
 */
function checkExistInDB(key, val, recallFunction)
{
	if (parent.frmHide == null || parent.frmHide.window == null) return;
	var url = "../pub/info.do?command=check_exist&key=" +
			  key + "&val=" + val + "&recall_function=" + recallFunction;
	parent.frmHide.window.location.href = url;
}


function alterLabelColor()
{
	var element = null;
	var err = document.all("error_td");
	for (var i=0; i<errors.length; i++)
	{
		element = document.all(errors[i] + "_LABEL");
		if (element != null)
		{
			element.className = "redfont";
			if (err != null)
			{
				err.innerHTML = replaceString(err.innerHTML, errors[i], element.innerText);
			}
		}
	}
}

function replaceString(str, source, target)
{
	var result = "";
	var begin = str.indexOf(source);
	if(begin == -1) return str;
	result += str.substring(0, begin);
	result += "<b>" + target + "</b>";
	result += str.substring(begin + source.length, str.length);
	return result ;
}

function showOverrideConfirm()
{
	var objDirectUpdate = document.all("info(IS_DIRECT_UPDATE)");
	//var confirmMsg = getMessage("MSG_0058"));
	var confirmMsg = getMessage("MSG_S0_0023");
	if(confirm(confirmMsg))
	{
		if(objDirectUpdate != null)
			objDirectUpdate.value = true;
			
		execute("modify_save_override");
	}
	else
	{
		if(objDirectUpdate != null)
			objDirectUpdate.value = false;
	}
}

function showInfoDialog(msg, obj)
{
	alert(msg);
	if(obj != null)
	{
		obj.focus();
		obj.select();
	}
}

function getColorByID(colorId)
{
	var result = "#FFFFFF";
	switch (colorId)
	{
		case "1": // yellow
			result = "#FCFCCA";
			break;
		case "2": // red
			result = "#FCC0C1";
			break;
		case "3": // green
			result = "#C6FCC0";
			break;		
		case "4": // blue
			result = "#C0D6FC";
			break;			
		case "5": // orange
			result = "#FCD8C0";
			break;
		case "6": // cambridge blue
			result = "#E0EDFF";
			break;
		case "7": // shadow red
			result = "#FED9DA";
			break;
		case "8": // shadow cambridge blue
			result = "#EBF2FB";
			break;			
		case "9": // progress revise color
			result = "#00AAFF";
			break;
		case "10": // progress return color
			result = "#0066FF";
			break;
		case "11": // progress over page color
			result = "#FF0000";
			break;
		case "12": // edi accept overdue color
			result = "#FF0000";
			break;	
		default:  // general color
			break;
	}
	return result;
}

function getBgColor(obj)
{	
	var result = "#FFFFFF";
	if (obj == null) return result;
	if (isSelectedRecord(obj)) 
	{
		return "#FFFF00";
	}

	return getColorByID(obj.bgcolor_id);
}

function setTdBgColor(obj)
{	
	if (obj == null) return;
	
	if (obj.tagName.toLowerCase() == "td")
	{
		var colorid = obj.bgcolor_id;
		if (colorid != null && colorid != "")
		{
			obj.bgColor = getColorByID(colorid);
		}
	}
}

function initTdColor(tdId)
{
	if (tdId == null || tdId == "")
	{
		tdId = "colortd";
	}
	var tds = document.all(tdId);
	if (tds == null) return;
	
	var count = tds.length;		
	if (count > 0) // td array
	{
		for (var i = 0; i < count; i++)
		{
			setTdBgColor(tds[i]);
		}
	} else { // only one td
		setTdBgColor(tds);
	}
}

function initColor()
{
	var table = document.getElementById("resultTable");
	//if (table==null) return;
	if (table==null) alert("Your searching result table name must be \"resultTable\"");

	for (var i=1; i<table.rows.length; i++)
	{
		setBgColor(table.rows[i]);
	}
	
}


function setBgColor(obj)
{
	if (obj==null) return;
	var tr = null;
	if (obj.tagName.toLowerCase() == "td")
		tr = obj.parentElement;
	else if (obj.tagName.toLowerCase() == "tr")
		tr = obj;
	else
		return result;
	tr.bgColor = getBgColor(obj);
}

function isSelectedRecord(trortd)
{
	var result = false;
	if (document.all("chkEachs") == null) return result;
	var tr = null;
	var checkbox = null;
	var chks = document.all("chkEachs");
	if (trortd.tagName.toLowerCase() == "td")
		tr = trortd.parentElement;
	else if (trortd.tagName.toLowerCase() == "tr")
		tr = trortd;
	else
		return result;
	
	if (chks.type == "checkbox")
		checkbox = chks;
	else
		checkbox = chks[tr.rowIndex-1];
	return checkbox.checked;
}

function showInputOpFinishedInfo(msg, type, isconfirmUpdate)
{	
	// show infomation massage
	if(msg != null && msg != "" && (type == null || type == ""))
	{
		showInfoDialog(msg);
	}
	
	// change errors label color and replace parameter of message
	alterLabelColor();
	if(isconfirmUpdate != null && isconfirmUpdate == "true")
	{
		showOverrideConfirm();
	}
}

function toUpper(obj)
{
	if(obj != null)
	{
		var value = obj.value;
		var upperValue = value.toUpperCase();
		obj.value = upperValue;
	}
}

function isLegalSpaceCode(obj)
{
	var result = true;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == null || value == "") return true;

    for(var i=0; i<value.length; i++)
    {
        var strSub = value.substring(i,i+1);         
        /*if( !((strSub<="9"&&strSub>="0") || 
		    (strSub<="Z"&&strSub>="A") || 
			(strSub<="z"&&strSub>="a") ||
			(strSub == "-") ||
			(strSub == "/")) )*/
		if( !(strSub<="~" && strSub>="!") ||
			  strSub=="'" ||
			  strSub=="%" ||
			  strSub=="?")
        {
			alert(getMessage("MSG_S0_0025"));
			obj.focus();
			obj.select();
			result = false;
			break;
        }
    }
	return result;	
}
/**
 * begin, end "yyyy/MM"
*/
function countMonths(begin, end)
{
	if (begin == null || end == null) return null;
	if (begin > end) return null;
	if (begin.length < 7 || end.length < 7) return null;
	var result = 0;
	var beginYear = parseInt(begin.substring(0, 4));
	var endYear = parseInt(end.substring(0, 4));
	var beginMonth = 0;
	var endMonth = 0;
	if (begin.substring(5, 6) == 0)
		beginMonth = parseInt(begin.substring(6, 7));
	else
		beginMonth = parseInt(begin.substring(5, 7));
		
	if (end.substring(5, 6) == 0)
		endMonth = parseInt(end.substring(6, 7));
	else
		endMonth = parseInt(end.substring(5, 7));
	
	result = (endYear - beginYear) * 12 + (endMonth - beginMonth) + 1;
	return result;
}

function isBeginLessEndDate(beginObj, endObj)
{
	if (beginObj == null || endObj == null) return null;
	if (beginObj.value == null || endObj.value == null) return null;
	var result = beginObj.value > endObj.value;
	if (result)
	{
		alert(getMessage("MSG_S0_0026"));
		beginObj.select();
		beginObj.focus();
	}
	return !result;
}

function enableButton(command, isEnable, btnObj)
{
	var img1 = "";
	var img3 = "";
	var className = "cursor";	
	var button = btnObj;
	if (button == null)
	{
		var btnName = "Btn" +  command.substring(0, 1).toUpperCase();
		btnName += command.substring(1, command.length).toLowerCase();
		button = document.all(btnName);
	}
	
	if (button == null || button.tagName == null)  return;

	if (button.tagName.toUpperCase() == "IMG")
	{
		img1 = "../images/btn_" + command.toLowerCase() + "1.gif";
		img3 = "../images/btn_" + command.toLowerCase() + "3.gif";
	}
	
	
	if (isEnable) // enable the button
	{			
		button.disabled = false;
		if (button.tagName.toUpperCase() == "IMG")
		{
			button.src = img1;
			button.className = className;					
		}
	}
	else         // disable the button
	{			
		button.disabled = true;
		if (button.tagName.toUpperCase() == "IMG")
		{
			button.src = img3;
			button.className = "";				
		}
	}
}

function enableRefButton(btnName, isEnable)
{
	var className = "cursor";	
	var button = document.all(btnName);
	
	if (button == null || button.tagName == null)  return;
	if (isEnable) // enable the button
	{			
		button.disabled = false;
		if (button.tagName.toUpperCase() == "IMG")
		{
			button.className = className;					
		}
	}
	else         // disable the button
	{			
		button.disabled = true;
		if (button.tagName.toUpperCase() == "IMG")
		{
			button.className = "";				
		}
	}
}


/*
 * Convert string to uppercase.
 */
function convertToUpperCase(obj)
{
	if(obj == null) return;
	
	if(obj.value.trim() == "") return;
	
	var content = obj.value;
	obj.value = content.toUpperCase();
}

function hasCondition(objArray,isShowAlert)
{
	if (objArray == null)
	{
		return true;
	}
	var result = false;	
	
	for (var i = 0; i < objArray.length; i++)
	{
		var obj = document.all(objArray[i]);
		if ( obj.value != null && obj.value.trim() != "")
		{
			result = true;
			break;
		}
	}
	if (!result && isShowAlert)
	{
		alert(getMessage("MSG_S0_0054"));
		//alert("need condition" + objArray[0]);
		document.all(objArray[0]).focus();
	}
	return result;
}

/**
  * check file type
  * @param fileName: file name
  * @param fileExt: file extent name (not include ".")
  */
function checkFileType(fileName, fileExt)
{
	var result = false;
	var n = fileName.lastIndexOf(".");
	var len = fileName.length;
	if (n >= 0 && n < len)
	{
		var sub = fileName.substring(n + 1, len);
		result = sub.equalsIgnoreCase(fileExt);
	}
	return result;
}

function trimLeft(src)
{
	var result = "";
	if(src == null || src == "")
		return src;
	
	var len = src.length;
	for(var i=0; i<len; i++)
	{
		 var strSub = src.substring(i,i+1); 
		 if(strSub != " " &&
			strSub != "　" &&
			strSub != "	")
		 {
			 result = src.substring(i, len);
			 break;
		 }
	}
	return result;
}

function trimRight(src)
{
	var result = "";
	if(src == null || src == "")
		return src;
	
	var len = src.length;
	for(var i=len; i>0; i--)
	{
		 var strSub = src.substring(i-1, i); 
		 if(strSub != " " &&
			strSub != "　" &&
			strSub != "	")
		 {
			 result = src.substring(0, i);
			 break;
		 }
	}
	return result;
}

function trimAll(src)
{
	var res = trimLeft(src);
	res = trimRight(res);
	return res;
}


function isIPObjPub(obj, msgID)
{
	if(obj == null) return;	
	if(obj.value.trim() == "") return;
	
	var content = obj.value.trim();
	var contentlength = content.length;
	var num = 0;
	var dian = 0;
	
	for(var i=0; i<contentlength; i++)
	{
		var char = content.substring(i, i+1);

		if(!isIPDigital(char))
		{
			if(char == '.')
			{
				dian ++;
				if(i-num > 3 || i-num < 1)
				{
					alert(getMessage(msgID));
					obj.focus();
					return;
				}
				else
					num = i + 1;
			}
		}
		else
		{
			alert(getMessage(msgID));
		 	obj.focus();
			return;
		}
	}
	
	if(dian != 3)
	{
		alert(getMessage(msgID));
		obj.focus();
		return;
	}
}

function isIPDigital(char)
{
	//if (char < '0' || char > '9')
	if (char == '.' || char == '0' || char == '1' || char == '2' || char == '3' || char == '4'
	 || char == '5' || char == '6' || char == '7' || char == '8' || char == '9')
	{
		return false;
	}
	return true;
}

 
/**
 * Check whether input is numeric. if not, focus the field, and make the filed selected
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isStringNumObjPub(obj, msgID)
{
	var result = false;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;
	
	
	result = isStringNum(value);
	if (!result)
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();
	}
	return result;
}

/**
 * Check whether input is digital or letter string. 
 * if not, focus the field, and make the filed selected.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isDigitalOrLetterObjPub(obj, msgID)
{
	var result = false;
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;

	result = isDigitalOrLetter(value);
	if (!result)
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();
	}
	return result;
}

function isYearObjPub(obj, msgID)
{
	var value = obj.value;
	value = value.trim();
	if (value =="")
	 return true;
	if (value.length>4)
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();
		return false;
	}
	
	var today = new Date();
	var year = today.getYear()+"";
	
	result = isStringNum(value);
	if (result)
	{
		if (value.length < 4)		
		value = year.substring(0,(4-value.length)) + value;
		obj.value = value;
		return true;
	}
	else
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();
		return false;
	} 
		
	return true;
}

/**
 * Check whether input is date string. 
 * if not, focus the field, and make the filed selected.
 * if is, format date string.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isDateObjPub(obj, msgID)
{
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;
	
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
		//alert(getMessage("MSG_S0_0014"));
		alert(getMessage(msgID)); // alter by yaoxm 2005/05/20
		obj.focus();
		obj.select();		
		return false;
	}
	
	if (!isDateString(formatedDate, "-"))
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedDate;
	return true;
}

function isYYYYMMDateObjPub(obj, msgID)
{
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;
	if (value == "0")
	{
		var today = new Date();
		var month = today.getMonth()+1+"";
		value = (month.length == 1) ? ("0" + month) : month;
	}
	value = value.replace(/\//gi,"");
	value += "01";
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();	
		return false;
	}
	
	if (!isDateString(formatedDate, "-"))
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedDate.substring(0, 7);
	return true;
}

/**
 * Check whether input is time string. 
 * if not, focus the field, and make the filed selected.
 * if is, format date string.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isTimeObjPub(obj, msgID)
{
	if (obj == null) return false;
	
	var value = obj.value.trim();
	if (value == "") return true;
	
	var formatedTime = formatTime(value, ":");	
	if (formatedTime == null)
	{
		//alert(getMessage("MSG_S0_0014"));
		alert(getMessage(msgID));
		return false;
	}
	
	if (!isTimeString(formatedTime, ":"))
	{
		alert(getMessage(msgID));
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedTime;
	return true;
}

/**
 * Check enter e-mail addresss 
 * the e-mail address is must contain "@" and "." 
 * if the e-mail don't contain above two char then return false
 * else return true 
 */
function isEmailObjPub(obj, msgID) 
{
	emailPattern = /.*\@.*\..*/;
	if (obj == null) return false;
	var value = obj.value.trim();
	if (value == "") return true;

	if (!emailPattern.test(value))
	{
		alert(getMessage(msgID));
		obj.select();
		obj.focus();
		return false;
	}
	return true;
}

/*
 * validate a Tel string .
 * @param String sTel string Tel to validate
  * 
 * @return boolean
 */
function isTelObjPub(obj, msgID)
{
	if (obj == null) return false;
	var value = obj.value.trim();
	if (value == "") return true;
	
	var result = true;
	reg = /^\d|^-|^\(|^\)/;
    for(var i=0; i<value.length; i++)
    {
        var strSub = value.substring(i,i+1);         //char
		if (!reg.test(strSub))
		{
			result = false;
			break;
		}
    }
	if (!result)
	{
		alert(getMessage(msgID));
		obj.select();
		obj.focus();		
	}
	return result;
}

/*
 * validate a post string .
 * @param String sTel string Tel to validate
  * 
 * @return boolean
 */
function isPostObjPub(obj, msgID)
{
	if (obj == null) return false;
	var value = obj.value.trim();
	if (value == "") return true;
	
	var result = true;
	reg = /^\d|^-/;
    for(var i=0; i<value.length; i++)
    {
        var strSub = value.substring(i,i+1);         //char
		if (!reg.test(strSub))
		{
			result = false;
			break;
		}
    }
	if (!result)
	{
		alert(getMessage(msgID));
		obj.select();
		obj.focus();		
	}
	return result;
}




/*
 * validate a date
 * @param String sDate string date to validate
 * @param String sSpit split
 * @return boolean
 */
function isDateString(sDate, sSpit)
{	var iaMonthDays = [31,28,31,30,31,30,31,31,30,31,30,31];
	var iaDate = new Array(3);
	var year, month, day;

	iaDate = sDate.split(sSpit);
	if (iaDate.length != 3) return false;
	if (iaDate[1].length > 2 || iaDate[2].length > 2) return false;
	
	//check numeric
	if(!isStringNum(iaDate[0]) || !isStringNum(iaDate[1]) || !isStringNum(iaDate[2]) )
	{
		return false;
	}

	year = parseFloat(iaDate[0]);
	month = parseFloat(iaDate[1]);
	day = parseFloat(iaDate[2]);

	if (year < 1900 || year > 2200) return false;
	if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) iaMonthDays[1]=29;
	if (month < 1 || month > 12) return false;
	if (day < 1 || day > iaMonthDays[month - 1]) return false;
	return true;
}

/**
 * format date string to YYYY/MM/DD
 * @param  dateString  
 * @param  spit
 * @return boolean  
 * @note   call this function in onblur
 */
function formatDate(dateString, spit)
{
	var result = null;
	if(!isStringNum(dateString)) 
	{
		result = dateString;
		var iaDate = new Array(3);
		iaDate = result.split(spit);
		if (iaDate.length == 3)
		{
			var reyear = iaDate[0];
			var remonth = iaDate[1];
			var reday = iaDate[2];
			if(remonth.length == 1) remonth = "0"+remonth;
			if(reday.length == 1) reday = "0"+reday;
			result = reyear + spit + remonth + spit + reday;
		}
		return result;
	}
	
	var today = new Date();
	var year = today.getYear()+"";
	var month = today.getMonth()+1+"";
	var day = today.getDate()+"";
	month = (month.length == 1) ? ("0" + month) : month;
	day = (day.length == 1) ? ("0" + day) : day;
	
	switch (dateString.length)
	{
		case 1:  // d or today
			if(dateString == "0")	// 0 for today
				result = year + spit + month + spit + day;
			else
				result = year + spit + month + spit + "0" + dateString;
			break;
		case 2:  // dd
			result = year + spit + month + spit + dateString;
			break;
		case 3:  // mdd
			result = year + spit + "0" + dateString.substring(0, 1) + spit + dateString.substring(1, 3);
			break;
		case 4:  // mmdd
			result = year + spit + dateString.substring(0, 2) + spit + dateString.substring(2, 4);
			break;
		case 5:  // ymmdd
			result = year.substring(0,3) + dateString.substring(0, 1) + spit + 
					dateString.substring(1, 3) + spit + dateString.substring(3, 5);
			break;
		case 6:  // yymmdd
			year = dateString.substring(0, 2);
			year = (parseFloat(year) > 70)?("19" + year):("20" + year);
			result = year + spit + dateString.substring(2, 4) + spit +
					 dateString.substring(4, 6);
			break;
		case 7:  // yyymmdd
			year = dateString.substring(0, 3);
			year = (parseFloat(year) < 100)?("2" + year):("1" + year);
			result = year + spit + dateString.substring(3, 5) + spit +
					 dateString.substring(5, 7);
			break;
		case 8:  // yyyymmdd
			result = dateString.substring(0, 4) + spit + dateString.substring(4, 6) + spit +
					 dateString.substring(6, 8);
			break;
	}
	
	return result;
}

/**
 * Check whether input is date string. 
 * if not, focus the field, and make the filed selected.
 * if is, format date string.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isDateObj(obj,blank)
{
	if (obj == null) return false;
	
	obj.value  = obj.value.trim();
	var value = obj.value.trim();
	if (value == "") {
		if(blank == null) return true;
		if(blank !="false"){
		alert(getMessage("MSG_SYS_037"));
		}
		obj.focus();
		obj.select();
		return false;
	}
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
	    if(blank !="false"){
		alert(getMessage("MSG_SYS_004"));
		}
		obj.focus();
		obj.select();
		return false;
	}
	if (!isDateString(formatedDate, "-"))
	{
	    if(blank !="false"){
		alert(getMessage("MSG_SYS_024"));
		}
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedDate;
	return true;
}

//return true or false
function check_DateObj(obj,blank)
{
	if (obj == null) return false;
	
	obj.value  = obj.value.trim();
	var value = obj.value.trim();
	if (value == "") {
		if(blank == null)
			 return true;
		
		return false;
	}
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
		return false;
	}
	if (!isDateString(formatedDate, "-"))
	{
		return false;
	}
	
	obj.value = formatedDate;
	return true;
}

function check_DateObj_setFocus(obj,setFocusId , blank)
{
	if (obj == null) return false;
	
	obj.value  = obj.value.trim();
	var value = obj.value.trim();
	if (value == "") {
		if(blank == null)
			 return true;
		
		return false;
	}
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
		return false;
	}
	if (!isDateString(formatedDate, "-"))
	{
		return false;
	}
	
	obj.value = formatedDate;
	
	if(setFocusId != null && setFocusId != "")
	{
		var setFocusObj = document.getElementById(setFocusId);
		setFocusObj.focus();
		setFocusObj.select();
	}
	
	return true;
}


function isYYYYMMDateObj(obj)
{
	if (obj == null) return false;
		
	var value = obj.value.trim();
	if (value == "") return true;
	if (value == "0")
	{
		var today = new Date();
		var month = today.getMonth()+1+"";
		value = (month.length == 1) ? ("0" + month) : month;
	}
	value = value.replace(/-/gi,"");
	value += "01";
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
		alert(getMessage("MSG_SYS_004"));
		return false;
	}
	
	if (!isDateString(formatedDate, "-"))
	{
		alert(getMessage("MSG_SYS_037"));
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedDate.substring(0, 7);
	return true;
}

function isYYYYDateObj(obj)
{
	if (obj == null) return false;
	var value = obj.value.trim();
	obj.value = value;
	if (value == "") return true;
	if (value == "0")
	{
		var today = new Date();
		value = today.getYear();
	}
	
	value += "0101";
	var formatedDate = formatDate(value, "-");	
	if (formatedDate == null)
	{
		alert(getMessage("MSG_SYS_004"));
		return false;
	}
	
	if (!isDateString(formatedDate, "-"))
	{
		alert(getMessage("MSG_SYS_037"));
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedDate.substring(0, 4);
	return true;	
}

/*
 * validate a time string it must be like 15:30
 * @param String sTime string time to validate
 * @param String sSpit split
  * 
 * @return boolean
 */
function isTimeString(sTime, sSpit)
{
	var iaTime = new Array(2);
	var hour, min, sec;

	iaTime = sTime.split(sSpit);
	if (iaTime.length != 3) return false
	if (iaTime[0].length > 2 || iaTime[1].length > 2 || iaTime[2].length > 2) return false
	
	//check numeric
	if(!isStringNum(iaTime[0]) || !isStringNum(iaTime[1]) || !isStringNum(iaTime[2]))
	{
		return false;
	}

	hour = parseInt(iaTime[0]);
	min = parseInt(iaTime[1]);
	sec = parseInt(iaTime[2]);
	if (hour < 0 || hour > 23) return false;
	if(min < 0 || min > 59) return false;		
	if(sec < 0 || sec > 59) return false;	
	return true
}

/**
 * format time string to HH:MI
 * @param  time  
 * @param  spit
 * @return boolean   
 * @note   call this function in onblur
 */
function formatTime(timeString, spit)
{
	var result = null;
	if(timeString.indexOf(spit) >= 0)
	{
		var iaTime = new Array(3);
		var hour, min, sec;	
		iaTime = timeString.split(spit);
		if(iaTime.length == 3)
		{
			timeString = (iaTime[0].length == 1 ? "0"+iaTime[0] : iaTime[0]) + spit
						+(iaTime[1].length == 1 ? "0"+iaTime[1] : iaTime[1]) + spit
						+(iaTime[2].length == 1 ? "0"+iaTime[2] : iaTime[2]);
		}
	}
	
	if(!isStringNum(timeString)) return timeString;
	
	var today = new Date();
	var hour = today.getHours() + "";
	var minute = today.getMinutes() + "";
	var second = today.getSeconds() + "";
	hour = (hour.length == 1) ? ("0" + hour) : hour;
	minute = (minute.length == 1) ? ("0" + minute) : minute;
	second = (second.length == 1) ? ("0" + second) : second;
	
	switch (timeString.length)
	{
		case 1:  // H or today
			if(timeString == "0")	// 0 for today
				result = hour + spit + minute + spit + second;
			else
				result = "0" + timeString + spit + "00" + spit + "00";
			break;
		case 2:  // HH
			//result = timeString + spit + "00";
			result = timeString + spit + "00" + spit + "00";
			break;
		case 3:  // HHM
			result = timeString.substring(0, 2) + spit + timeString.substring(2, 3) + "0" + spit + "00";
			break;
		case 4:  // HHMM
			result = timeString.substring(0, 2) + spit + timeString.substring(2, 4) + spit + "00";
			break;
		case 5:  // HHMMS
			result = timeString.substring(0, 2) + spit + timeString.substring(2, 4) + spit + timeString.substring(4, 5) + "0";
			break;
		case 6:  // HHMMSS
			result = timeString.substring(0, 2) + spit + timeString.substring(2, 4) + spit + timeString.substring(4, 6);
			break;
	}
	return result;
}

/**
 * Check whether input is time string. 
 * if not, focus the field, and make the filed selected.
 * if is, format date string.
 * @param  obj  
 * @return boolean  
 * @note   call this function in onblur
 */
function isTimeObj(obj)
{
	if (obj == null) return false;
	
	var value = obj.value.trim();
	obj.value = value;
	if (value == "") return true;
	
	var formatedTime = formatTime(value, ":");
	
	if (formatedTime == null)
	{
		alert(getMessage("MSG_SYS_004"));
		return false;
	}
	
	if (!isTimeString(formatedTime, ":"))
	{
		alert(getMessage("MSG_SYS_038"));
		obj.focus();
		obj.select();		
		return false;
	}
	
	obj.value = formatedTime;
	return true;
}

/**
 * 检验日期范围是否合法
 * @param obj1 起始日期DOM对象
 * @param obj2 结束日期DOM对象
 * @param isMustInput 是否是必输项布尔值  不输入默认为 false
 * @return 两个对象是否有值，并且是否是日期 YYYY/MM/DD格式，起始是否小于结束日期
 */
function checkDateObjScope(obj1, obj2, isMustInput)
{
	if(typeof(isMustInput) == "undefined" || isMustInput == null)
		isMustInput = false;
	
	if(isMustInput && (obj1.value == "" || obj2.value == ""))
	{
		alert(getMessage("MSG_SYS_061"));
		return false;
	}
	if(!isDateObj(obj1) || !isDateObj(obj2))
		return false;
	
	if(obj2.value!="" && obj2.value < obj1.value)
	{
		alert(getMessage("MSG_SYS_023"));
		return false;
	}
	return true;
}

/**
*
*@param1 str 字符串
*@param2 n   最大长度
*@return 字符串是否超过最大长度（中文占三个字符）
*/
function isLenExceed(str,n) {
	if(typeof(str) == 'undefined' || str == null) {
		return false;
	}
	//v = str.trim();
	var len = 0;
	for(var ix=0; ix<str.length;ix++) {
		var _char = str.charCodeAt(ix);
		   if(!(_char>255)){
		      len = len + 1;
		   } else {
			  len = len + 3;
		   }
	}
	return len>n;
}

/**
 * 检查对象是否为空
 * @param obj DOM控件
 * @param labelName 标题字符
 * @return 为空时提示 [XXX]不能为空
 */
function isEmptyObj(obj,labelName){
	try{
		if(obj.value.trim() == ""){
			alert(getMessage("MSG_SYS_063",labelName));
			obj.focus();
			return true;
		}
	}catch(e){
		alert(e);
	}
	return false;
}


/**
 * 去掉数字前的0
 * @param obj
 * @return
 */
function removeZeroFirst(obj){
	var num_value = obj.value;
	if(num_value.length>0){
		
	
	var num = /^([0-9][0-9]*\.?\d{0,2})$/;
	
	if(num.test(num_value)){
		var num_1 = /^([0-9]*)$/;
		var num_double = /^(0[0-9]+\.?\d{0,2})$/;
		if(num_1.test(num_value)){
			
			if(num_value.indexOf(0)==0){
				
				obj.value =num_value.substring(1,num_value.length);
				
			}
		}
		if(num_double.test(num_value)){
			
			obj.value  =num_value.substring(1,num_value.length);
		}
	}else{
		alert("请输入数字");
		return;
	}
	}

}




//计算字符长度(一个汉字算3个字符)
function charLength(obj){
    var tel = /^[\u4e00-\u9fa5]+$/;
    var   pat  =   '？。，；：‘’“”、|——《》！￥]';        
    var num =/^[0-9]$/;
    var letter = /^[A-Za-z]$/;
   	var count = 0;
   	if(obj.value==''){
   		count = 0;
   	}else{
   		for(var i=0 ; i< obj.value.length ; i++){
   			var ch = obj.value.charAt(i);
   			if(tel.test(ch)){
   				count +=3;
   			}else if(num.test(ch)){
   				count +=1;
   			}else if(letter.test(ch)){
   				count +=1;
   			}
   			else if(pat.indexOf(ch)){
   				count +=3;
   			}
   		}
   	}
   	return count;
}

//check  A标签 
function checkA(obj){
   var htmlVal=obj.val();
   var div=$("<div>");
   div.html(htmlVal);
   
   var aBoolean=true;
   // 遍历 A标签 
   div.find("a").each(function(){
   var src= $(this).attr("href");
   
      if(src.indexOf("http://")==-1){
        //无 http://
         for(var i=0;i<src.length;i++){
            if(src.charAt(i)=="/"){
             var tempdomain=src.substring(0,i);
             if(isTrueDomain(tempdomain)){
             	return true;
             }else{
				aBoolean=false;
             	return false;
             }
             break;
            }
          } 
      }else{
        for(var i=7;i<src.length;i++){
            if(src.charAt(i)=="/"){
             var tempdomain=src.substring(7,i);
             if(isTrueDomain(tempdomain)){
             	return true;
             }else{
				aBoolean=false;
             	return false;
             }
             break;
            }
        }
  
     }
   });
   return aBoolean;
}

//check Img 标签
function checkImg(obj){
   var htmlVal=obj.val();
   var div=$("<div>");
   div.html(htmlVal);
   
   var aBoolean=true;
   // 遍历Img标签 
   div.find("img").each(function(){
   var src= $(this).attr("src");
      if(src.indexOf("http://")==-1){
        //无 http://
         for(var i=0;i<src.length;i++){
            if(src.charAt(i)=="/"){
             var tempdomain=src.substring(0,i);
             if(isTrueDomain(tempdomain)){
             	return true;
             }else{
				aBoolean=false;
             	return false;
             }
             break;
            }
          } 
      }else{
        for(var i=7;i<src.length;i++){
            if(src.charAt(i)=="/"){
             var tempdomain=src.substring(7,i);
             if(isTrueDomain(tempdomain)){
             	return true;
             }else{
				aBoolean=false;
             	return false;
             }
             break;
            }
        }
  
     }
   });
   return aBoolean;
}

// check True domain
function isTrueDomain(domain){
   var  result=false;
   var bg="bg.bbc.com";
   var sbg="sbg.bbc.com";
   var bgsit="bg.bbcsit.com";
   var sbgsit="sbg.bbcsit.com";
   var bguat="bg.bbcuat.com";
   var sbguat="sbg.bbcuat.com";
   var bgprp="bg.bbcprp.com";
   var sbgprp="sbg.bbcprp.com";
   
   var domain1="www.gome.com.cn";
   
   var domain2="img.gomein.net.cn";
   
   var domain3="img.agtsit.net.cn";
   
   var domain4="img.agtuat.net.cn";
   
   var  domain5="img.gomeprelive.net.cn";
   
   srcList.push(bg);
   srcList.push(sbg);
   
   srcList.push(bgsit);
   srcList.push(sbgsit);
   
   srcList.push(bguat);
   srcList.push(sbguat);
   
   srcList.push(bgprp);
   srcList.push(sbgprp);
 
   srcList.push(domain1);
   srcList.push(domain2);
   srcList.push(domain3);
   srcList.push(domain4);
   srcList.push(domain5);
   
   for(var j=1;j<15;j++)
   {
      srcList.push("img"+j+".gomein.net.cn");
      srcList.push("img"+j+".agtsit.net.cn");
      srcList.push("img"+j+".agtuat.net.cn");
      srcList.push("img"+j+".gomeprelive.net.cn");
   }
    
   for(var i=0;i<srcList.length;i++){
      if(domain==srcList[i]){
        result=true;
        break;
      }
      
   }
   return result;
} 

//check A超链接
function  checkHasA(obj){
	 var htmlVal=obj.val();
	   var div=$("<div>");
	   div.html(htmlVal);
	   
	   var aBoolean=false;
	   // 遍历 A标签 
	   div.find("a").each(function(){
		   aBoolean=true;
		   return;
	   });
	 return aBoolean;
}
//验证商城商品只能为数字
function checkNumberBySku(obj){
	if(obj.val() == '' || obj == null){return true}
    var reg = /^[0-9]+$/;
    if(!reg.test(obj.val())){
        alert('商城商品编码只能输入数字！');
        obj.val("");
        obj.focus();
        return false;
    }
    return true
}



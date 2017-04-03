if(jQuery && jQuery.validator){
    jQuery.extend(jQuery.validator.messages, {
        required: "该字段不能为空",
        remote: "数据有误",
        email: "请输入一个有效的邮件地址",
        url: "请输入一个有效的URL",
        date: "请输入一个有效的日期",
        dateISO: "Please enter a valid date (ISO).",
        number: "请输入一个有效的数字",
        digits: "该字段只能由数字组成",
        creditcard: "Please enter a valid credit card number.",
        equalTo: "与之前的输入不一致",
        accept: "该文件类型不被支持",
        maxlength: jQuery.validator.format("最多输入 {0} 个字符"),
        minlength: jQuery.validator.format("最少输入 {0} 个字符"),
        rangelength: jQuery.validator.format("请输入 {0} 至 {1} 个字符"),
        range: jQuery.validator.format("请输入介于 {0} 和 {1} 之间的值"),
        max: jQuery.validator.format("请输入大于或等于 {0} 的值"),
        min: jQuery.validator.format("请输入小于或等于 {0} 的值")
    });
}

function refreshValidateimg(trigger,imgid){
    var url = '/Vercode.aspx?w=85&h=34';
    var trigger_type = trigger.tagName.toLowerCase();
    if(trigger_type=='img'){
        var img = jq(trigger);
    }else{
        if(!imgid) imgid = 'validatecodeimg';
        var img = jq('#'+imgid);
    }
    if(!jq('#'+imgid+':visible').length){
        img.show();
        img.closest('div').find('input').attr('placeholder','输入右边图片中字符');
    }else if(trigger_type=='input') return;
    img.attr('src',url+'&t='+new Date().getTime());
}

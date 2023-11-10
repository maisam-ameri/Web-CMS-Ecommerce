
CKEDITOR.editorConfig = function (config) {
	config.removeButtons = 'Underline,Subscript,Superscript';
	config.uiColor = '#F7B42C';
	config.height = 300;
	config.toolbarCanCollapse = true;

	config.filebrowserUploadUrl = '/upload-file';
};
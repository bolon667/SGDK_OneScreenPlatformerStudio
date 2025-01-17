using Godot;
using System;

public class ChooseFileProperty : HBoxContainer
{
	private Node singleton;
	private FileDialog fileDialog;
	private Label infoLabel;
	private String workingDir;
	
	public int levelInd;


	[Export] private String levelAttrName = "";
	[Export] private String fileDialogLocalPath = "/code_template/customScripts/";
	[Export] private String defaultVal = "NULL";
	
	public void loadContent(){
		//nothing
	}
	
	public override void _Ready()
	{
		singleton = (Node)GetNode("/root/singleton");
		fileDialog = (FileDialog)GetNode("FileDialog");
		infoLabel = (Label)GetNode("VBoxContainer/infoLabel");
		workingDir = System.IO.Directory.GetCurrentDirectory();
		String projectFolderPath = (String)singleton.Call("get_project_folder_path");
		fileDialog.CurrentPath = projectFolderPath + fileDialogLocalPath;
		
		levelInd = int.Parse(singleton.Get("cur_level_ind").ToString());
	}

	//  // Called every frame. 'delta' is the elapsed time since the previous frame.
	//  public override void _Process(float delta)
	//  {
	//      
	//  }
	private void _on_FileDialog_file_selected(String path)
	{
		String fileName = path.Substring(path.FindLast("/")+1);
		String fileNameNoExt = fileName.Substring(0,fileName.Find("."));
		GD.Print(fileNameNoExt);
		infoLabel.Text = fileNameNoExt;
		singleton.Call("change_level_attr", levelAttrName, fileNameNoExt, levelInd);
	}
	
	private void _on_Button_button_down()
	{
		fileDialog.PopupCentered();
	}
	
	private void _on_removeBtn_button_down()
	{
		singleton.Call("change_level_attr", levelAttrName, "", levelInd);
		infoLabel.Text = defaultVal;
	}
	
	private void _on_applyAllLevelsBtn_button_down()
	{
		singleton.Call("change_all_level_attr", levelAttrName, infoLabel.Text);
	}
	
	private void _on_makeDefaultBtn_button_down()
	{
		singleton.Call("change_default_level_attr", levelAttrName, infoLabel.Text);
		
	}
}













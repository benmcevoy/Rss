import config from "/app/config.js";

'use strict';

const e = React.createElement;

class TreeView extends React.Component {
	constructor(props) {
		super(props);

		this.state = {};

		// bind ensures "this" is always this class instance
		// bind is a javascript concern, class methods are unbound by default
		this.get = this.get.bind(this);

		/* TODO:
			- introduce JSX and node, build 🤦‍♀️
			DONE - view model, mock, actually i can just call the api
			IGNORED - project viewmodel from api resonse
			DONE - render the data raw as a check
			- render the real markup, composition of <folder>, <item>
			- how do i get the <TreeView> tag?\
			- allow multiple treeview's with some config (props)
			- state - selectedItem and the traversal of the children to get there
			- child items are folder and feed/item/leaf
		
		TreeView?
		
		Root: Title
			
			Folder
				Id
				Title
				Count of children
				expandCollapse()
				select()
					raises event ItemSelected(id, folder)
					sets selected state
		
				has a bunch of child Feed items
		
		
			and can have a bunch of loose items that are not in a folder
			Feed
				Id
				Title
				Count
				select()
					raises event ItemSelected(id, feed)
					sets selected state
		
		
		
		
		*/
	}

	componentDidMount(){
		this.get();
	}

	render() {
		if(this.state.viewModel === undefined){
			return "Loading...";
		}
		
		return "TODO: treeview.";
	}

	get() {
		fetch(config.api.folder.getRoot)
			.then(x => x.json())
			.then(x => this.setState({ viewModel: x }));
	}

}

const domContainer = document.querySelector('#treeview');
ReactDOM.render(e(TreeView), domContainer);
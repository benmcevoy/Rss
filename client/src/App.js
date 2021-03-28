import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import React from "react";
import TwoColumn from "./Layouts/TwoColumn/component";
import AppContext from "./AppContext";

import TreeView from "./Components/TreeView/component.js";
import Menu from "./Components/Menu/component.js";
import List from "./Components/List/component";

export default class App extends React.Component {
    constructor(props) {
        super(props)

        this.updateCurrent = this.updateCurrent.bind(this);

        this.state = {
            current: {
                folder: null,
                feed: null,
                item: null
            },
            updateCurrent: this.updateCurrent
        };
    }

    updateCurrent = (folderId, feedId, itemId) => {
        console.log(`${folderId ?? null}, ${feedId?? null}, ${itemId?? null}`);

        this.setState({
            current: {
                folder: folderId ?? null,
                feed: feedId ?? null,
                item: itemId ?? null
            }
        });
    }

    render = () => 
        <AppContext.Provider value={this.state}>
            <TwoColumn
                header={<Menu />}
                aside={<TreeView title="Subscriptions" />}
                main={<List title="Stream" />}
            />
        </AppContext.Provider>
}

rss.models.Item = function (id, name, count) {
    var self = this;
    self.id = id;
    self.name = name;
    self.count = ko.observable(count);

    self.items = ko.observableArray([
    ]);

}
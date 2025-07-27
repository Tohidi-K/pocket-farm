mergeInto(LibraryManager.library, {
    SyncFileSystem: function () {
        FS.syncfs(false, function (err) {
            if (err) {
                console.error("Sync error:", err);
            } else {
                console.log("Filesystem synced successfully.");
            }
        });
    }
});
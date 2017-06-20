
DELETE FROM ADFolderAccesses
FROM            ADDrives INNER JOIN
                         ADFolders ON ADDrives.Id = ADFolders.DriveId INNER JOIN
                         ADFolderAccesses ON ADFolders.Id = ADFolderAccesses.FolderID
WHERE        (ADDrives.Id = 2)
DELETE FROM ADFolders
FROM            ADDrives INNER JOIN
                         ADFolders ON ADDrives.Id = ADFolders.DriveId
WHERE        (ADDrives.Id = 2)
DELETE FROM ADDrives
WHERE        (Id = 2)
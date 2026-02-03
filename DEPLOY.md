## Release and Deployment Guide

---

### 1. Initial Release

* **Update Version:** Search and replace old version strings with the new version.
* **Build:** Generate the installer executable.
* **GitHub:** Create a new release and upload the installer and the portable zip file

---

### 2. Winget Update

Generate the manifest using `wingetcreate`:

```bash
wingetcreate update PatrickDemichiel.VolumeScroller --version 2.3.0 --urls "https://github.com/patrickiel/VolumeScroller/releases/download/2.3.0/Volume.Scroller.2.3.0.exe|x64" --out .

```

---

### 3. Sync Repository

Before submitting, update your fork at [github.com/patrickiel/winget-pkgs](https://github.com/patrickiel/winget-pkgs):

1. Click **Sync fork**.
2. Click **Update branch**.

---

### 4. Validation & Testing

**Validate** the manifest structure:

```bash
winget validate manifests\p\PatrickDemichiel\VolumeScroller\2.3.0

```

**Test** the installation locally:

1. Enable local manifests (Admin Terminal):
```bash
winget settings --enable LocalManifestFiles

```


2. Run installation (Normal Terminal):
```bash
winget install --manifest manifests\p\PatrickDemichiel\VolumeScroller\2.3.0

```



---

### 5. Submission

Submit the package to the official repository:

```bash
wingetcreate submit manifests\p\PatrickDemichiel\VolumeScroller\2.3.0

```

Would you like me to create a simplified GitHub Action template to automate the version replacement and release steps?
<template>
    <div>
        <form>
            <input type="file" @change="onFileChange">
        </form>
        <div v-if="errorMessage">
            {{errorMessage}}
        </div>
    </div>
</template>

<script>
import { mapActions } from 'vuex'
export default {
    name: 'UploadCsv',
    data() {
        return {
            errorMessage: ""
        }
    },
    methods: {
        ...mapActions(['uploadCsv']),
        onFileChange(e) {
            var files = e.target.files || e.dataTransfer.files;
            console.log(files)
            if (!files.length)
                return;
            console.log(files[0].name)
            if (!files[0].name.includes(".csv")) {
                this.errorMessage = "Please enter CSV file"
                return;
            }
            else {
                this.errorMessage = ""
            }

            this.uploadCsv(files[0]);
        },
    },
}
</script>
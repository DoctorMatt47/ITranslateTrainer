<script lang="ts">
  export let value: number;
  export let className = "";

</script>

<input {...$$restProps}
       bind:value
       class="input text-center p-1 dark:bg-black/0 placeholder:text-surface-600 {className}"
       type="number"
/>

<style>
    input::-webkit-outer-spin-button,
    input::-webkit-inner-spin-button {
        /* display: none; <- Crashes Chrome on hover */
        -webkit-appearance: none;
        margin: 0; /* <-- Apparently some margin are still there even though it's hidden */
    }

    input[type=number] {
        -moz-appearance: textfield; /* Firefox */
    }
</style>
